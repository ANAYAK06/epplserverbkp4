
ALTER Procedure   [dbo].[spCheckConfigStatus]   @Code VARCHAR(100),
												@AllCode VARCHAR(100)=null,
											     @ChkStatus INT OUTPUT
AS
BEGIN
BEGIN TRY
BEGIN TRANSACTION
      IF(@Code='Term Loan Payment')
	  Begin
	    IF EXISTS(SELECT * FROM TermLoanPayment WHERE Status not in ('Approved','Rejected'))
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Term Loan Creation')
	  Begin
	    IF EXISTS(SELECT * FROM TermLoanCreation WHERE Status not in ('Approved','Rejected'))
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Open FD')
	  Begin
	    IF EXISTS(SELECT * FROM FDPayments WHERE Status not in ('Approved','Rejected'))
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	    IF(@Code='FD Recieve Interest')
	  Begin
	    IF EXISTS(SELECT * FROM FDPayments WHERE Status not in ('Approved','Rejected'))
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	    IF(@Code='FD Recieve Principle')
	  Begin
	    IF EXISTS(SELECT * FROM FDPayments WHERE Status not in ('Approved','Rejected'))
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Item Code Creation Assets')
	  Begin
	    Declare @Indtypeid1 varchar(50)
		set @Indtypeid1=(Select ConfigurationTypeId from ConfigurationType where Type='Assets')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtypeid1)
	    --IF EXISTS(SELECT * FROM ItemCodes WHERE substring(itemcode,1,1)='1')
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Item Code Creation Semi Assets/Consumables')
	  Begin
	    Declare @Indtypeid2 varchar(50)
		set @Indtypeid2=(Select ConfigurationTypeId from ConfigurationType where Type='Semi Assets/Consumables')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtypeid2)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Item Code Creation Consumables')
	  Begin
	    Declare @Indtypeid3 varchar(50)
		set @Indtypeid3=(Select ConfigurationTypeId from ConfigurationType where Type='Consumables')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtypeid3)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Item Code Creation BoughtOutItems')
	  Begin
	    Declare @Indtypeid4 varchar(50)
		set @Indtypeid4=(Select ConfigurationTypeId from ConfigurationType where Type='BoughtOutItems')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtypeid4)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Item Code Creation Raw Material')
	  Begin
	    Declare @Indtypeid5 varchar(50)
		set @Indtypeid5=(Select ConfigurationTypeId from ConfigurationType where Type='Raw Material')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtypeid5)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Indent Type Define By')
	  Begin
	    Declare @Indtypeid varchar(50)
		set @Indtypeid=(Select ConfigurationTypeId from ConfigurationType where ConfigurationCategory='IndentType')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtypeid)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='New Stock Issued By')
	  Begin
	    Declare @Indtype varchar(50)
		set @Indtype=(Select ConfigurationTypeId from ConfigurationType where ConfigurationCategory='NewStock')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Indtype and ConfigurationValue is not null)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Transport')
	  Begin
	    Declare @Trantype varchar(50)
		set @Trantype=(Select ConfigurationTypeId from ConfigurationType where Type='Transport' and ConfigurationCategory='DCA')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Trantype and ConfigurationValue is not null)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
	  IF(@Code='Transport')
	  Begin
	    Declare @Trantype1 varchar(50)
		set @Trantype1=(Select ConfigurationTypeId from ConfigurationType where Type='Transport'and ConfigurationCategory='SDCA')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@Trantype1 and ConfigurationValue is not null)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	   End
	  IF(@Code='Output GST' and @AllCode='Output GST DCA')
	  Begin
	    Declare @OG varchar(50)
		set @OG=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='DCA')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@OG and ConfigurationValue is not null)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	   End	  
	  IF(@Code='Output GST' and @AllCode='Output GST SDCA CGST')
		  Begin
			Declare @OCGST varchar(50)
			set @OCGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@OCGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	  IF(@Code='Output GST' and @AllCode='Output GST SDCA SGST')
		  Begin
			Declare @OSGST varchar(50)
			set @OSGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@OSGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	 IF(@Code='Output GST' and @AllCode='Output GST SDCA IGST')
		  Begin
			Declare @OIGST varchar(50)
			set @OIGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@OIGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	  IF(@Code='Input GST' and @AllCode='Input GST DCA')
		  Begin
			Declare @IG varchar(50)
			set @IG=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='DCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@IG and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	  IF(@Code='Input GST' and @AllCode='Input GST SDCA CGST')
		  Begin
			Declare @CGST varchar(50)
			set @CGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@CGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	  IF(@Code='Input GST' and @AllCode='Input GST SDCA SGST')
		  Begin
			Declare @SGST varchar(50)
			set @SGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@SGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	 IF(@Code='Input GST' and @AllCode='Input GST SDCA IGST')
		  Begin
			Declare @IGST varchar(50)
			set @IGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@IGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	  IF(@Code='Output TCS' and @AllCode='Output TCS DCA')
	  Begin
	    Declare @TOG varchar(50)
		set @TOG=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='DCA')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@TOG and ConfigurationValue is not null)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	   End	  
	  IF(@Code='Output TCS' and @AllCode='Output TCS SDCA')
		  Begin
			Declare @TOCGST varchar(50)
			set @TOCGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@TOCGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
		IF(@Code='Input TCS' and @AllCode='Input TCS DCA')
		  Begin
			Declare @TSGST varchar(50)
			set @TSGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='DCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@TSGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	 IF(@Code='Input TCS' and @AllCode='Input TCS SDCA')
		  Begin
			Declare @TIGST varchar(50)
			set @TIGST=(Select ConfigurationTypeId from ConfigurationType where ConfigurationName=@AllCode and ConfigurationCategory='SDCA')
			IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@TIGST and ConfigurationValue is not null)
			SET @ChkStatus = 1;
			Else
			SET @ChkStatus = 0;
		   End
	  IF(@Code='Salary Define By')
	  Begin
	    Declare @SIndtypeid varchar(50)
		set @SIndtypeid=(Select ConfigurationTypeId from ConfigurationType where ConfigurationCategory='Salary')
	    IF EXISTS(select * from Configurations WHERE ConfigurationtypeId=@SIndtypeid)
		SET @ChkStatus = 1;
		Else
		SET @ChkStatus = 0;
	  End
COMMIT TRANSACTION
END TRY
BEGIN CATCH
DECLARE @ModuleName    VARCHAR(50),
                @ProcedureName VARCHAR(50),
                @ErrorNumber VARCHAR(10),
				@ErrorMessage  VARCHAR(300),
				@ErrorSeverity TINYINT,
				@ErrorState    TINYINT

        SELECT  @ModuleName = 'spCheckConfigStatus',
                @ProcedureName = ERROR_PROCEDURE(),
                @ErrorNumber = ERROR_NUMBER(),
                @ErrorMessage = ERROR_MESSAGE(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE();
		
		IF @@TRANCOUNT > 0  
			ROLLBACK TRANSACTION 

		PRINT 'Transaction Rolledback Successfully.'

        RAISERROR (@ErrorMessage,@ErrorSeverity,@ErrorState);
END CATCH
END