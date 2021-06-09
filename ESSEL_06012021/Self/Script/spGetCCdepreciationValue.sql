CREATE Procedure   [dbo].[spGetCCdepreciationValue] 
@CCode varchar(100)
As
Begin
  Declare @CCValue varchar(100)
  IF EXISTS(SELECT * FROM CCAccruedValues where ReportType='Depreciation'  and CCCode=@CCode and InterestRate =0)
  BEGIN
  set @CCValue=(select isnull(value,0)  from CCAccruedValues where ReportType='Depreciation'  and CCCode=@CCode and InterestRate =0)
 
  if(@CCValue is null)
  Select ccvalue='0'
  Else
  select ccvalue=@CCValue
  END
  ELSE
  Select ccvalue='0'
End

--exec spGetCCdepreciationValue
--select REPLACE(CONVERT(VARCHAR, GETDATE(), 106), ' ', '-')




