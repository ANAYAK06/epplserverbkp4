CREATE procedure   [dbo].[spInsertDepValue]			@Id INT,
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
  IF(@Action='Depvalue')
  BEGIN
   IF Not Exists (Select * from CCAccruedValues where CCCode=@CCCode and Reporttype=@Reporttype)
	   BEGIN 
		  Insert into CCAccruedValues(CCCode,Value,ReportType,Createdby,CreatedDate)
		  Values(@CCCode,@Amount,@Reporttype,@Createdby,GETDATE()) 
		  SET @AddUpdateStatus ='Submited';
	  END 
  ELSE
      BEGIN
         update CCAccruedValues set Value=@Amount where CCCode=@CCCode
	     SET @AddUpdateStatus ='Submited';
	END
  END
  ELSE
  BEGIN
     Insert into CCAccruedValues(InterestRate,ReportType,Createdby,CreatedDate,WefDate)
     Values(@Interest,@Reporttype,@Createdby,GETDATE(),@WEF)
	 SET @AddUpdateStatus ='Submited';
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

        SELECT  @ModuleName = 'spInsertAccurateValue',
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







