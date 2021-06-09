ALTER Procedure   [dbo].[spGetCCOverheadpercent] 
@CCode varchar(100)
As
Begin
  Declare @CCValue varchar(100)
  IF EXISTS(SELECT * FROM CCAccruedValues where ReportType='Overhead'  and CCCode=@CCode)
  BEGIN
  set @CCValue=(select isnull(interestrate,0)  from CCAccruedValues where ReportType='Overhead'  and CCCode=@CCode)
 
  if(@CCValue is null)
  Select ccvalue='0'
  Else
  select ccvalue=@CCValue
  END
  ELSE
  Select ccvalue='0'
End

--exec spGetCCOverheadpercent 'CC-141'
--select REPLACE(CONVERT(VARCHAR, GETDATE(), 106), ' ', '-')




