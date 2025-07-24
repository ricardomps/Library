import config from '../config/settings';

const API_BASE_URL = config.apiSettings.libraryUrl;

const booksService = {
    getBooks: async (searchTerm = '') => {
        try {
            const url = searchTerm
                ? `${API_BASE_URL}/books?searchTerm=${encodeURIComponent(searchTerm)}`
                : `${API_BASE_URL}/books`;
            const response = await fetch(url);
            if (!response.ok) throw new Error('Failed to fetch books');
            return await response.json();
        } catch (error) {
            console.error('Error fetching books:', error);
            throw error;
        }
    },

    createBook: async (bookData) => {
        try {
            const response = await fetch(`${API_BASE_URL}/books`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bookData),
            });
            if (!response.ok) throw new Error('Failed to create book');
            return await response.json();
        } catch (error) {
            console.error('Error creating book:', error);
            throw error;
        }
    },

    updateBook: async (id, bookData) => {
        try {
            const response = await fetch(`${API_BASE_URL}/books/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bookData),
            });
            if (!response.ok) throw new Error('Failed to update book');
            return await response.json();
        } catch (error) {
            console.error('Error updating book:', error);
            throw error;
        }
    },

    deleteBook: async (id) => {
        try {
            const response = await fetch(`${API_BASE_URL}/books/${id}`, {
                method: 'DELETE',
            });
            if (!response.ok) throw new Error('Failed to delete book');
            return response.status === 204;
        } catch (error) {
            console.error('Error deleting book:', error);
            throw error;
        }
    },

    borrowBook: async (id) => {
        try {
            const response = await fetch(`${API_BASE_URL}/books/${id}/borrow`, {
                method: 'POST',
            });
            if (!response.ok) throw new Error('Failed to borrow book');
            return response.status === 204;
        } catch (error) {
            console.error('Error borrowing book:', error);
            throw error;
        }
    },

    returnBook: async (id) => {
        try {
            const response = await fetch(`${API_BASE_URL}/books/${id}/return`, {
                method: 'POST',
            });
            if (!response.ok) throw new Error('Failed to return book');
            return response.status === 204;
        } catch (error) {
            console.error('Error returning book:', error);
            throw error;
        }
    }
};

export default booksService;