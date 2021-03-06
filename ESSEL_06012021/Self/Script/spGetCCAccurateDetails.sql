USE [ServerDBEsselV2_22122020]
GO
/****** Object:  StoredProcedure [dbo].[spGetCCAccurateDetails]    Script Date: 06-01-2021 09:33:52 AM ******/
ALTER procedure [dbo].[spGetCCAccurateDetails] @Type VARCHAR(50)
AS
BEGIN
  if(@Type='None')
  BEGIN
  Select id Id,CCCode CostCenter,CC_Name CCName,replace(isnull(Value,0),'.0000','.00')  Amount,InterestRate,ReportType 
  from CCAccruedValues ca 
  join Cost_Center cc on cc.cc_Code=ca.CCCode where CC_Status='Open' and Status='Approved' 
  END
  ELSE IF(@Type='Accrued')
  BEGIN
    Select id Id,CCCode CostCenter,CC_Name CCName,replace(isnull(Value,0),'.0000','.00')  Amount,InterestRate,ReportType 
  from CCAccruedValues ca 
  join Cost_Center cc on cc.cc_Code=ca.CCCode where CC_Status='Open' and Status='Approved'  and ReportType='Accrued'
  END
   ELSE IF(@Type='Depreciation')
  BEGIN
    Select id Id,CCCode CostCenter,CC_Name CCName,replace(isnull(Value,0),'.0000','.00')  Amount,InterestRate,ReportType 
  from CCAccruedValues ca 
  join Cost_Center cc on cc.cc_Code=ca.CCCode where CC_Status='Open' and Status='Approved'   and ReportType='Depreciation'
  END
  ELSE IF(@Type='Overhead')
  BEGIN
    Select id Id,CCCode CostCenter,CC_Name CCName,replace(isnull(Value,0),'.0000','.00')  Amount,InterestRate,ReportType 
  from CCAccruedValues ca 
  join Cost_Center cc on cc.cc_Code=ca.CCCode where CC_Status='Open' and Status='Approved'   and ReportType='Overhead'
  END
END
