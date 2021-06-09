CREATE Procedure   [dbo].[spGetwefdatedepreciation] 
As
Begin
Declare @Date varchar(50)
  set @Date=(select top 1 REPLACE(CONVERT(VARCHAR, wefdate, 106), ' ', '-')  from CCAccruedValues where ReportType='Depreciation'  AND CCCode IS NULL order by id desc )
 
  if(@Date is null)
  Select wefdate='01-Apr-2019'
  Else
  select wefdate=REPLACE(CONVERT(VARCHAR, DATEADD(DAY, 1, @Date), 106), ' ', '-')

End

--exec spGetwefdatedepreciation
--select REPLACE(CONVERT(VARCHAR, GETDATE(), 106), ' ', '-')
--select * from CCAccruedValues




