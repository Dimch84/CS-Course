﻿using System.Collections;
using System.Collections.Generic;

public class Library : IEnumerable<Book>
{
    private IList<Book> books;

    public Library(params Book[] books)
    {
        this.books = books;
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return this.books.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}