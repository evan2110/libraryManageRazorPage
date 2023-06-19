using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;

namespace ProjectPRN221.Pages
{
    public class ExcelModel : PageModel
    {
        IBookRepository bookRepository = new BookRepository();
        IAccountRepository accountRepository = new AccountRepository();

        public FileContentResult OnGet(string? mode)
        {
                if(mode == "export")
                {
                var allBooks = bookRepository.GetBooks();

                var fileName = "books.xlsx";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Books" + DateTime.Now);

                    worksheet.Cells[1, 1].Value = "Title";
                    worksheet.Cells[1, 2].Value = "Author";
                    worksheet.Cells[1, 3].Value = "Publication Date";
                    worksheet.Cells[1, 4].Value = "AvailableCopies";
                    worksheet.Cells[1, 5].Value = "TotalCopies";
                    worksheet.Cells[1, 6].Value = "ShelfLocation";

                    for (int i = 0; i < allBooks.Count; i++)
                    {
                        var book = allBooks[i];
                        worksheet.Cells[i + 2, 1].Value = book.Title;
                        worksheet.Cells[i + 2, 2].Value = book.Author;
                        worksheet.Cells[i + 2, 3].Value = book.PublicationDate.ToShortDateString();
                        worksheet.Cells[i + 2, 4].Value = book.AvailableCopies;
                        worksheet.Cells[i + 2, 5].Value = book.TotalCopies;
                        worksheet.Cells[i + 2, 6].Value = book.ShelfLocation;
                    }

                    package.Save();
                }
                return File(System.IO.File.ReadAllBytes(filePath), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            return null;
                
        }

        
    }
}
