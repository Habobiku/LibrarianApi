# RestAPI with AWS DynamoDB


//Get the book by ket and name
GET http://testtask.azurewebsites.net/api/GetBook?key=Title&id=Superfreakonomics
{
    "date": "2022-03-02",
    "title": "Superfreakonomics",
    "author": "Dubner",
    "isbn": "2993120",
    "publisher": "HarperCollins",
    "status": "Available ",
    "id": 4,
    "genre": "economics"
}

//Get all books
GET http://testtask.azurewebsites.net/api/GetBooks

{
    "listBooks": [
        {
            "id": 7,
            "title": "Integration of the Indian States",
            "author": "Menon",
            "publisher": "Orient",
            "status": "Reserved",
            "isbn": "1793129",
            "date": "2007-07-08",
            "genre": "history"
        },
        {
        ...
        },
       
        {
            "id": 5,
            "title": "Orientalism",
            "author": "Said",
            "publisher": "Penguin",
            "status": "Borrowed",
            "isbn": "2793120",
            "date": "2010-09-23",
            "genre": "history"
        }
    ]
}

//Get status of book by title
GET http://testtask.azurewebsites.net/api/GetStatus?Title=Data Smart
{
    "title": "Data Smart",
    "status": "Borrow"
}


//Update status of book by id 
PUT http://testtask.azurewebsites.net/api/UpdateStatus?action=Borrow&id=5
return OK(Successful have been updated status to Borrow)

//Post the book
POST http://testtask.azurewebsites.net/api/PostBook?Title=Outsider&Author=Camus&Status=Reserve&Publisher=Penguin&Isbn=1793111&Genre=data_science&Date=2009-01-23
retrun OK(Successful have been added.Your id=954)
