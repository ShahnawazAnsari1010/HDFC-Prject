USE [HDFC_LIve]
GO
/****** Object:  StoredProcedure [dbo].[sp_createCourier]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================

-- Author:		<Author,,Name>

-- Create date: <Create Date,,>

-- Description:	<Description,,>

-- =============================================

CREATE PROCEDURE [dbo].[sp_createCourier] 

	@courier VARCHAR(200),
	@oId VARCHAR(250) OUTPUT
	AS

BEGIN

		INSERT INTO CourierMaster(Courier)
		VALUES (@courier);
		  SET @oId = 1

END 
 


GO
/****** Object:  StoredProcedure [dbo].[sp_InwardUpload]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InwardUpload] 

		  @AGEEMENTNO Numeric(18,0),
		  @BARCODE nvarchar(100),
		  @PRODUCTNAME nvarchar(200),
          @DISBURSEMENTDATE nvarchar(200),
          @CUSTOMERNAME nvarchar(200),
          @SYSTEM nvarchar(10),
          @CASESTARTERBRANCH nvarchar(50),
          @GENERATEDON float,
          @CPUID nvarchar(10),
          @batch_id nvarchar(200),
          @RepaymentMode nvarchar(10),
          @VendorLocation nvarchar(200),
          @VendorID int--,
			--@oId int OUTPUT,
			--@ParLngId int
		  
	AS

BEGIN
declare @ParLngId int
SET @ParLngId = (select count(*) from [dbo].[InwardManualUpload] where [AGEEMENTNO]=@AGEEMENTNO )
    --print( @ParLngId)
	if(@ParLngId<1)
	begin 

print('insert')
		INSERT INTO [dbo].[InwardManualUpload]
           ([AGEEMENTNO]
           ,[BARCODE]
           ,[PRODUCTNAME]
           ,[DISBURSEMENTDATE]
           ,[CUSTOMERNAME]
           ,[SYSTEM]
           ,[CASESTARTERBRANCH]
           ,[GENERATEDON]
           ,[CPUID]
           ,[batch_id]
           ,[RepaymentMode]
           ,[VendorLocation]
           ,[VendorID]
		   ,Created_date
		   ,updated_date)
     VALUES
           (
		   
		   @AGEEMENTNO ,
		   @BARCODE ,
		   @PRODUCTNAME ,
		   cast(@DISBURSEMENTDATE -2 as datetime),--convert(datetime,@DISBURSEMENTDATE,103),
		   @CUSTOMERNAME ,
		   @SYSTEM ,
		   @CASESTARTERBRANCH ,
		   cast(@GENERATEDON -2 as datetime),--convert(datetime,@GENERATEDON,103),
		   @CPUID ,
		   @batch_id ,
		   @RepaymentMode ,
		   @VendorLocation ,
		   @VendorID, 
		   getdate(),getdate());
		-- SET @oId = 1
	end	  
		
    else 
      Begin
	  print('update')
		update [dbo].[InwardManualUpload] set [BARCODE]=@BARCODE,[PRODUCTNAME]=@PRODUCTNAME,[DISBURSEMENTDATE]= cast(@DISBURSEMENTDATE -2 as datetime)
				   ,[CUSTOMERNAME]=@CUSTOMERNAME,[SYSTEM]=@SYSTEM,[CASESTARTERBRANCH]=@CASESTARTERBRANCH,[GENERATEDON]=cast(@GENERATEDON -2 as datetime)
				   ,[CPUID]=@CPUID,[batch_id]=@batch_id,[RepaymentMode]=@RepaymentMode,[VendorLocation]=@VendorLocation,[VendorID]=@VendorID
				   ,updated_date=getdate() where [AGEEMENTNO]=@AGEEMENTNO
     End
END 
 
GO
/****** Object:  StoredProcedure [dbo].[sp_PDCUpload]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PDCUpload] 

		  @AGREEMENTID Numeric(18,0),
		  @APPROVALDATE float,
		  @SecurityPDCCHQCount int
			--@oId int OUTPUT,
			--@ParLngId int
		  
	AS

BEGIN
declare @ParLngId int
SET @ParLngId = (select count(*) from [dbo].[PDCUpload] where [AGREEMENTID]=@AGREEMENTID )
    --print( @ParLngId)
	if(@ParLngId<1)
	begin 

print('insert')
		INSERT INTO [dbo].[PDCUpload]
           ([AGREEMENTID],
		    [APPROVALDATE],
		   [SecurityPDCCHQCount]
		   )
     VALUES
           (
		   @AGREEMENTID ,
		   cast(@APPROVALDATE -2 as datetime) ,
		   @SecurityPDCCHQCount
		   );
		-- SET @oId = 1
	end	  
		
    else 
      Begin
	  print('update')
		update [dbo].[PDCUpload] set  [APPROVALDATE]=cast(@APPROVALDATE -2 as datetime),
		   [SecurityPDCCHQCount]=@SecurityPDCCHQCount
		   where [AGREEMENTID]=@AGREEMENTID
     End
END 
 
GO
/****** Object:  StoredProcedure [dbo].[sp_updateCourier]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROC [dbo].[sp_updateCourier] 



	@Courier VARCHAR(200),
	@id INT,
	@oId VARCHAR(250) OUTPUT

AS


UPDATE [CourierMaster]
SET Courier=@Courier

WHERE id = @id;



IF (@@Error = 0)



BEGIN



	SET @oId = '1'



END

GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteCourier]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author:		<Author,,Name>

-- Create date: <Create Date,,>

-- Description:	<Description,,>

-- =============================================

Create PROCEDURE [dbo].[usp_DeleteCourier]

	@Id INT,
	@Msg Varchar(250) OUTPUT

AS

BEGIN
 
	SET NOCOUNT ON;

	Delete FROM CourierMaster  WHERE id = @id;  
	IF (@@Error = 0)
 
BEGIN 
	SET @Msg = 'Deleted successfully' 

END
 
 

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCourier]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================

-- Author:		<Author,,Name>

-- Create date: <Create Date,,>

-- Description:	<Description,,>

-- =============================================

Create PROCEDURE [dbo].[usp_GetCourier] 

	@Id INT = NULL

AS

BEGIN

	SET NOCOUNT ON;
	 
	SELECT U.id,u.Courier as Courier
	FROM [dbo].[CourierMaster]AS U
	WHERE U.id=@Id OR @Id is null

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCourierDetails]    Script Date: 19-10-2022 17:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================

-- Author:		<Author,,Name>

-- Create date: <Create Date,,>

-- Description:	<Description,,>

-- =============================================

Create PROCEDURE [dbo].[usp_GetCourierDetails] 

	@Id INT = NULL

AS

BEGIN

	SET NOCOUNT ON;
	 
	SELECT u.ID,u.Courier
	FROM [dbo].[CourierMaster]AS U

	WHERE U.id=@Id OR @Id is null

END
GO
