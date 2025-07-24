import React, { useState, useEffect } from 'react';
import BookList from './components/BookList';
import BookForm from './components/BookForm';
import SearchBar from './components/SearchBar';
import booksService from './services/booksService';

function App() {
  const [books, setBooks] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [loading, setLoading] = useState(false);
  const [showForm, setShowForm] = useState(false);
  const [editingBook, setEditingBook] = useState(null);

  useEffect(() => {
    loadBooks();
  }, []);

  const loadBooks = async (term = '') => {
    setLoading(true);
    try {
      const data = await booksService.getBooks(term);
      setBooks(data);
    } catch (error) {
      alert('Failed to load books. Please check if the API is running.');
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = () => {
    loadBooks(searchTerm);
  };

  const handleSave = () => {
    setShowForm(false);
    setEditingBook(null);
    loadBooks(searchTerm);
  };

  const handleEdit = (book) => {
    setEditingBook(book);
    setShowForm(true);
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      try {
        await booksService.deleteBook(id);
        loadBooks(searchTerm);
      } catch (error) {
        alert('Failed to delete book.');
      }
    }
  };

  const handleBorrow = async (id) => {
    try {
      await booksService.borrowBook(id);
      loadBooks(searchTerm);
    } catch (error) {
      alert('Failed to borrow book.');
    }
  };

  const handleReturn = async (id) => {
    try {
      await booksService.returnBook(id);
      loadBooks(searchTerm);
    } catch (error) {
      alert('Failed to return book.');
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Library App</h1>
          <p className="text-gray-600">Manage your book collection</p>
        </div>

        <div className="mb-6">
          <div className="flex flex-col sm:flex-row gap-4 justify-between items-start sm:items-center">
            <SearchBar
              searchTerm={searchTerm}
              onSearchChange={setSearchTerm}
              onSearch={handleSearch}
            />
            <button
              onClick={() => {
                setEditingBook(null);
                setShowForm(!showForm);
              }}
              className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
            >
              {showForm ? 'Cancel' : '+ Add Book'}
            </button>
          </div>
        </div>

        {showForm && (
          <BookForm
            book={editingBook}
            onSave={handleSave}
            onCancel={() => {
              setShowForm(false);
              setEditingBook(null);
            }}
          />
        )}

        <BookList
          books={books}
          loading={loading}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onBorrow={handleBorrow}
          onReturn={handleReturn}
        />
      </div>
    </div>
  );
}

export default App;