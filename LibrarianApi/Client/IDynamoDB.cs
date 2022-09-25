using System;
using LibrarianApi.Models;
using LibrarianApi.Responce;

namespace LibrarianApi.Client
{
    public interface IDynamoDB
    {
        public Task<Book> GetBook(GetResponce get);
        public Task<Books> GetBooks();
        public Task<bool> PostBook(PostResponce post);
        public Task<bool> UpdateStatus(string id, string action);
    }
}

