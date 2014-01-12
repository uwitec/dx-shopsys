USE [mianis]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetPageData]    Script Date: 04/12/2012 13:41:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetPageData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetPageData]
GO

USE [mianDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetPageData]    Script Date: 04/12/2012 13:41:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--exec sp_GetPageData 'SELECT NewsID,lbid,title,AddTime FROM news ',3,2

CREATE PROCEDURE [dbo].[sp_GetPageData]

@SQLSTR VARCHAR(8000), /* ��ѯ��SQL��� */

@CURPAGE INT,           /* ��ǰҳ��λ�� */

@PAGESIZE INT           /* ҳ����ʾ���������� */

AS

BEGIN

SET NOCOUNT ON

DECLARE @P1 INT,   /* �α� */

   @ROWCOUNT int,

   @COUNTPAGE int,

   @CurP int

   EXEC sp_cursoropen @P1 OUTPUT, @SQLSTR, @scrollopt=1, @ccopt=1, @ROWCOUNT=@ROWCOUNT OUTPUT

   IF @ROWCOUNT % @PAGESIZE >0

    SET @COUNTPAGE = @ROWCOUNT/@PAGESIZE+1

   ELSE

    SET @COUNTPAGE = @ROWCOUNT/@PAGESIZE

   IF @CURPAGE > @COUNTPAGE

    SET @CURPAGE = @COUNTPAGE

SET @CurP = (@CURPAGE-1)*@PAGESIZE+1

   SET NOCOUNT OFF

   /*�����������*/

   SELECT @CURPAGE CURPAGE,@PAGESIZE PAGESIZE,@COUNTPAGE COUNTPAGE,@ROWCOUNT [ROWCOUNT] 

   /*��������¼��*/

   EXEC sp_cursorfetch @P1,16,@CurP,@PAGESIZE 

   /*����������������ֻ�ܹ����һ���¼��*/ 

   SET NOCOUNT on 

   EXEC sp_cursorclose @P1 /* �رղ��ͷ��αꡣ*/

END


GO


