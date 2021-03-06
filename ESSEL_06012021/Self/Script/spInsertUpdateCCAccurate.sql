ALTER procedure   [dbo].[spInsertUpdateCCAccurate]			@Id INT,
													@Reporttype VARCHAR(50),
													@CCCode VARCHAR(50)=null,
													@Interest FLOAT,
													@Amount MONEY=null,
													@WEF varchar(100)=null,
													@Createdby VARCHAR(50),													
													@Action VARCHAR(50),
													@RoleId INT=0,								
													@AddUpdateStatus VARCHAR(50) OUTPUT   
AS										
BEGIN
BEGIN TRY
BEGIN TRANSACTION 
  BEGIN

  BEGIN
  IF(@Action='Add')
  BEGIN
  IF Not Exists (Select * from CCAccruedValues where CCCode=@CCCode and Reporttype=@Reporttype)
  BEGIN 
  Insert into CCAccruedValues(CCCode,Value,InterestRate,ReportType,Createdby,CreatedDate,WefDate)
  Values(@CCCode,@Amount,@Interest,@Reporttype,@Createdby,GETDATE(),@WEF)
 
  SET @AddUpdateStatus ='Submited';
  END  
  ELSE  
  SET @AddUpdateStatus ='Cost Center Already Exist';
  END
  ELSE IF(@Action='Update')
  BEGIN
  IF Exists (Select * from CCAccruedValues where CCCode=@CCCode and Reporttype=@Reporttype)
  BEGIN
  if(@Reporttype='Overhead')
	  Begin
			Update CCAccruedValues set InterestRate=@Interest,ModifiedBy=@Createdby,ModifiedDate=GETDATE()
			where  CCCode=@CCCode and ReportType=@Reporttype;
	  End
  Else
    Begin
		Update CCAccruedValues set Value=@Amount,InterestRate=@Interest,ModifiedBy=@Createdby,ModifiedDate=GETDATE()
		 where  CCCode=@CCCode and ReportType=@Reporttype;
	End
  SET @AddUpdateStatus ='Submited';
  END
  ELSE
  BEGIN
  SET @AddUpdateStatus ='Cost Center Not Exist';
  END

  END
  END
  END	
COMMIT TRANSACTION
END TRY
BEGIN CATCH
SET @AddUpdateStatus = ERROR_MESSAGE();
DECLARE @ModuleName    VARCHAR(50),
                @ProcedureName VARCHAR(50),
                @ErrorNumber VARCHAR(10),
				@ErrorMessage  VARCHAR(300),
				@ErrorSeverity TINYINT,
				@ErrorState    TINYINT

        SELECT  @ModuleName = 'spInsertUpdateCCAccurate',
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







