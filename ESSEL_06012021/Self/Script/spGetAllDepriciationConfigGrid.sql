CREATE Procedure   [dbo].[spGetAllDepriciationConfigGrid]  
As
Begin

      select CCCode as CostCenter,(CASE WHEN Value='0.00' THEN '' ELSE Value END) as Value,CASE WHEN Value='0.00' THEN 'Depriciation Percentage' ELSE 'Depriciation Value' END as ReportType,REPLACE(CONVERT(VARCHAR, WefDate, 106), ' ', '-') as wefdate,InterestRate as InterestRate  from CCAccruedValues where ReportType='Depreciation'

End

--select * from CCAccruedValues
--exec spGetAllDepriciationConfigGrid