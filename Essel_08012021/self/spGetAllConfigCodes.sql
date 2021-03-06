ALTER Procedure   [dbo].[spGetAllConfigCodes]  
@Code varchar(100),
@Codetext varchar(100)=null,
@Codefor varchar(100)
--@Type varchar(100)
As
Begin
Declare @Configid int
Declare @DCACode varchar(100)
Declare @DCAID varchar(100)
Declare @SDCACode varchar(100)
Declare @ITID varchar(100)
Declare @Type varchar(100)
IF(@Codefor='Output GST' or @Codefor='Output TCS')
Set @Type='%Out%'
Else
Set @Type='%In%'
    if(@Code='CostCenter')
	 select CC_Code as acodeid,(CC_Code+' , '+CC_Name)as acodevalue from Cost_Center where CC_Status='open' and  Status='Approved' order by CC_Code asc
	Else if(@Code='DCA')
	   Begin
	   If(@Codefor='Input GST' or @Codefor='Output GST')
	   	select DCACode as acodeid,(DCACode+' , '+DCAName)as acodevalue from DCA where DCAStatus='open' and  Status='Approved' and DCATypeID=2 order by DCACode asc
	   Else
		select DCACode as acodeid,(DCACode+' , '+DCAName)as acodevalue from DCA where DCAStatus='open' and  Status='Approved' order by DCACode asc
	   End
	Else if(@Code='SDCA')
	     If((@Codefor='Input GST' or @Codefor='Output GST')or(@Codefor='Input TCS' or @Codefor='Output TCS'))
		 Begin
		    if exists(select * from [Configurations] c join ConfigurationType ct on c.ConfigurationtypeId=ct.ConfigurationTypeId where Configuration_For=@Codefor  and ct.ConfigurationCategory='DCA')
		       Begin
			    Set @Configid=(select ConfigurationTypeId from ConfigurationType where Configuration_For=@Codefor and ConfigurationCategory='DCA')
			    Set @DCACode=(Select  ConfigurationValue from [Configurations]c join ConfigurationType ct on ct.ConfigurationTypeId=c.ConfigurationtypeId where c.ConfigurationtypeId=@Configid )
				Set @DCAID=(Select DCAID from DCA where DCACode=@DCACode and status='Approved')
				select SubDCACode as acodeid,(SubDCACode+' , '+SubDCAName)as acodevalue from SubDCA where SubDcaStatus='open' and  Status='Approved' and DCAID=@DCAID and SubDCAName like @Type order by SubDCACode asc			  
			   End
		   Else
		        Select 'Please select DCA before SDCA' as acodevalue
		 End
		 Else
		 Begin 
		   if exists(select * from [Configurations] c join ConfigurationType ct on c.ConfigurationtypeId=ct.ConfigurationTypeId where Configuration_For=@Codefor  and ct.ConfigurationCategory='DCA' and ct.[type]=@Codetext)
		       Begin
			    Set @Configid=(select ConfigurationTypeId from ConfigurationType where Configuration_For=@Codefor and ConfigurationCategory='DCA' and [type]=@Codetext)
			    Set @DCACode=(Select  ConfigurationValue from [Configurations]c join ConfigurationType ct on ct.ConfigurationTypeId=c.ConfigurationtypeId where c.ConfigurationtypeId=@Configid )
				Set @DCAID=(Select DCAID from DCA where DCACode=@DCACode and Status='Approved')
				select SubDCACode as acodeid,(SubDCACode+' , '+SubDCAName)as acodevalue from SubDCA where SubDcaStatus='open' and  Status='Approved' and DCAID=@DCAID order by SubDCACode asc			  
			   End
		   Else
		        Select 'Please select DCA before SDCA' as acodevalue
	     End	 
	Else if(@Code='IT')
		 Begin
		   if exists(select * from [Configurations] c join ConfigurationType ct on c.ConfigurationtypeId=ct.ConfigurationTypeId where Configuration_For=@Codefor and ct.ConfigurationCategory='SDCA' and ct.[type]=@Codetext)
		       Begin
			    Set @Configid=(select ConfigurationTypeId from ConfigurationType where Configuration_For=@Codefor and ConfigurationCategory='SDCA' and [type]=@Codetext)
			    Set @SDCACode=(Select  ConfigurationValue from [Configurations]c join ConfigurationType ct on ct.ConfigurationTypeId=c.ConfigurationtypeId where c.ConfigurationtypeId=@Configid )
				Set @ITID=(Select ITCodeID from SubDCA where SubDCACode=@SDCACode)
				select IT_Code as acodeid,(IT_Code+' , '+IT_Name)as acodevalue from IT where ITStatus='open' and  Status='Approved' and IT_ID=@ITID  order by IT_Code asc		   
				End
				Else
		        Select 'Please select SDCA before IT' as acodevalue
	     End	
   Else if(@Code='IndentType' or @Code='Salary')
	     Begin 
		    select (CAST(UserRoleID as varchar(10))+','+UserRoleCode) as acodeid,(CAST(UserRoleID as varchar(10))++','+UserRoleCode) as acodevalue from UserRole where Status='Approved'
		 End 
    Else if(@Code='NewStock')
	     Begin 
		   if exists(select * from [Configurations] c join ConfigurationType ct on c.ConfigurationtypeId=ct.ConfigurationTypeId where Configuration_For='Indent Type Define By' and ct.ConfigurationCategory='IndentType' and ct.[type]='All Indent Type')
		    Begin
			Declare @CheckPrev Varchar(20)
			Declare @DefineLevel int;
			Declare @RID varchar(20); 
			Set @CheckPrev=(select SUBSTRING(ConfigurationValue,1,3) as IndentTypeDefine from [Configurations]c join ConfigurationType ct on ct.ConfigurationTypeId=ct.ConfigurationTypeId and type='NewStock' and c.ConfigurationCategory='NewStock')
			Set @RID=(Select SUBSTRING(configurationvalue,1,3) from [Configurations] where ConfigurationtypeId=28)
			Set @DefineLevel=(select LevelOfApproval from WorkFlowLevels where MasterOperationID=278 and UserRoleID=@RID)			
		    select (CAST(ur.UserRoleID as varchar(10))+','+UserRoleCode) as acodeid,(CAST(ur.UserRoleID as varchar(10))+','+UserRoleCode) as acodevalue from UserRole ur  where UserRoleID in (select UserRoleID from WorkFlowLevels where LevelOfApproval>@DefineLevel and MasterOperationID=278 ) and ur.Status='Approved'
			End
			Else
			Select 'Please select Indent Define Type' as acodevalue
		 End 
End