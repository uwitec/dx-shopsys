
/****** Object:  StoredProcedure [dbo].[sp_GetPageData]    Script Date: 01/15/2014 18:38:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--exec sp_GetPageData 'SELECT NewsID,lbid,title,AddTime FROM news ',3,2

CREATE PROCEDURE [dbo].[sp_GetPageData]

@SQLSTR VARCHAR(8000), /* 查询的SQL语句 */

@CURPAGE INT,           /* 当前页面位置 */

@PAGESIZE INT           /* 页面显示的数据行数 */

AS

BEGIN

SET NOCOUNT ON

DECLARE @P1 INT,   /* 游标 */

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

   /*首先输出参数*/

   SELECT @CURPAGE CURPAGE,@PAGESIZE PAGESIZE,@COUNTPAGE COUNTPAGE,@ROWCOUNT [ROWCOUNT] 

   /*其次输出记录集*/

   EXEC sp_cursorfetch @P1,16,@CurP,@PAGESIZE 

   /*可以输出多组参数，只能够输出一组记录集*/ 

   SET NOCOUNT on 

   EXEC sp_cursorclose @P1 /* 关闭并释放游标。*/

END



GO


