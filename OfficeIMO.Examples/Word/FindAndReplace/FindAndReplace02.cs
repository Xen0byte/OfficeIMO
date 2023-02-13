using System;
using OfficeIMO.Word;

namespace OfficeIMO.Examples.Word {
    internal static partial class FindAndReplace {
        internal static void Example_FindAndReplace02(string folderPath, bool openWord) {
            Console.WriteLine("[*] Creating standard document - Find & Replace");
            string filePath = System.IO.Path.Combine(folderPath, "Basic Document with table to replace text.docx");
            using (WordDocument document = WordDocument.Create(filePath)) {
                document.AddParagraph("Test Section");

                document.Paragraphs[0].AddComment("Przemysław", "PK", "This is my comment");

                document.AddParagraph("Test Section - another line");

                document.Paragraphs[1].AddComment("Przemysław", "PK", "More comments");

                document.AddParagraph("This is a text ").AddText("more text").AddText(" even longer text").AddText(" and Even longer right?");

                document.AddParagraph("this is a text ").AddText("more text 1").AddText(" even longer text 1").AddText(" and Even longer right?");


                var table = document.AddTable(3, 3);


                table.Rows[0].Cells[0].Paragraphs[0].AddText("Test Section");
                table.Rows[0].Cells[1].Paragraphs[0].AddText("Test Section");
                table.Rows[0].Cells[2].Paragraphs[0].AddText("Test ").AddText("Sect").AddText("ion");

                // we now ensure that we add bold to complicate the search
                document.Paragraphs[9].Bold = true;
                document.Paragraphs[10].Bold = true;

                document.Save(false);
            }

            using (WordDocument document = WordDocument.Load(filePath)) {
                document.CleanupDocument();

                var listFound = document.Find("Test Section");
                Console.WriteLine("Find (should be 5): " + listFound.Count);

                var replacedCount = document.FindAndReplace("Test Section", "Production Section");
                Console.WriteLine("Replaced (should be 5): " + replacedCount);

                document.Save(openWord);
            }
        }
    }
}
