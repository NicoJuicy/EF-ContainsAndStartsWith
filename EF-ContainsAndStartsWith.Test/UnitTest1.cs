using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EF_ContainsAndStartsWith.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void DummyForEFSeed()
        {
            using (var db = new EF_ContainsAndStartsWith.DomainAndData.Data.DataContext(true))
            {
                var products = db.Products.ToList();
            }
        }

        //does always generate EF Migrations :( --> reason why it's slower
        [TestMethod]
        public void TestNoContains()
        {
            /*
             Test Name:	TestNoContains
                Test Outcome:	Passed
                Result StandardOutput:	
                Debug Trace:
                Opened connection at 28/11/2016 16:36:33 +01:00


                Opened connection at 28/11/2016 16:36:33 +01:00
                SELECT 
                    [Extent1].[Id] AS [Id], 
                    [Extent1].[Name] AS [Name], 
                    [Extent1].[PriceExcl] AS [PriceExcl], 
                    [Extent1].[inStockAmount] AS [inStockAmount], 
                    [Extent1].[isActive] AS [isActive], 
                    [Extent1].[CategoryId] AS [CategoryId]
                    FROM  [dbo].[Products] AS [Extent1]
                    INNER JOIN [dbo].[Categories] AS [Extent2] ON [Extent1].[CategoryId] = [Extent2].[Id]
                    WHERE [Extent2].[Code] LIKE N'A%'
                -- Executing at 28/11/2016 16:36:33 +01:00
                -- Completed in 0 ms with result: SqlDataReader

                Closed connection at 28/11/2016 16:36:33 +01:00
             */
            using (var db = new EF_ContainsAndStartsWith.DomainAndData.Data.DataContext(true))
            {
                db.Database.Log = el => System.Diagnostics.Debug.Write(el);
                var productsFromCat = db.Products.Where(el => el.Category.Code.StartsWith("A")).ToList();
            }
        }


        //generated sql in comment ( doesn't generate EF migrations) 
        [TestMethod]
        public void TestWithContains()
        {

            /*
             Test Name:	TestWithContains
            Test Outcome:	Passed
            Result StandardOutput:	
            Debug Trace:
            Opened connection at 28/11/2016 16:34:21 +01:00
            SELECT 
                [Extent1].[Id] AS [Id], 
                [Extent1].[Name] AS [Name], 
                [Extent1].[PriceExcl] AS [PriceExcl], 
                [Extent1].[inStockAmount] AS [inStockAmount], 
                [Extent1].[isActive] AS [isActive], 
                [Extent1].[CategoryId] AS [CategoryId]
                FROM [dbo].[Products] AS [Extent1]
                WHERE  EXISTS (SELECT 
                    1 AS [C1]
                    FROM   ( SELECT 1 AS X ) AS [SingleRowTable1]
                    LEFT OUTER JOIN  (SELECT 
                        [Extent2].[Id] AS [Id], 
                        [Extent2].[Code] AS [Code]
                        FROM [dbo].[Categories] AS [Extent2]
                        WHERE [Extent1].[CategoryId] = [Extent2].[Id] ) AS [Project1] ON 1 = 1
                    WHERE ( CAST(CHARINDEX([Project1].[Code], N'A') AS int)) = 1
                )
            -- Executing at 28/11/2016 16:34:21 +01:00
            -- Completed in 0 ms with result: SqlDataReader

            Closed connection at 28/11/2016 16:34:21 +01:00


             */
            string[] categories = new string[] { "A" };
            using (var db = new EF_ContainsAndStartsWith.DomainAndData.Data.DataContext(true))
            {
                db.Database.Log = el => System.Diagnostics.Debug.Write(el);
                // var productsFromCat = db.Products.Any(el => categories.Contains(dl =>dl.Products.Category.Code.StartsWith(dl))).ToList();
                var productsFromCat = db.Products.Where(el => categories.Any(dl => dl.StartsWith(el.Category.Code))).ToList();
            }
        }
    }
}
