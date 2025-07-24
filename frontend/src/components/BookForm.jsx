import React, { useState, useEffect, useRef } from 'react';
import Dropdown from './Dropdown';
import booksService from '../services/booksService';
import resourcesService from '../services/resourcesService';

const BookForm = ({ book, onSave, onCancel }) => {
    const [formData, setFormData] = useState({
        title: '',
        author: '',
        publisher: ''
    });
    const [resources, setResources] = useState({ authors: [], publishers: [] });
    const [loading, setLoading] = useState(false);
    const hasLoadedResources = useRef(false);

    useEffect(() => {
        if (!hasLoadedResources.current) {
            loadResources();
            hasLoadedResources.current = true;
        }
    }, []);

    useEffect(() => {
        if (book) {
            setFormData({
                title: book.title || '',
                author: book.author || '',
                publisher: book.publisher || ''
            });
        }
    }, [book]);

    const loadResources = async () => {
        try {
            const data = await resourcesService.getResources();
            setResources(data);
        } catch (error) {
            console.error('Failed to load resources');
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        try {
            if (book) {
                await booksService.updateBook(book.id, formData);
            } else {
                await booksService.createBook(formData);
            }
            onSave();
            setFormData({ title: '', author: '', publisher: '' });
        } catch (error) {
            alert('Failed to save book. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="bg-white rounded-lg shadow-md p-6 mb-6">
            <h2 className="text-xl font-bold mb-4">
                {book ? 'Edit Book' : 'Add New Book'}
            </h2>

            <form onSubmit={handleSubmit}>
                <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Title *
                        </label>
                        <input
                            type="text"
                            name="title"
                            value={formData.title}
                            onChange={handleChange}
                            required
                            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                        />
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Author
                        </label>
                        <Dropdown
                            options={resources.authors?.map(author => ({ label: author, value: author }))}
                            selected={formData.author}
                            onChange={value => setFormData(prev => ({ ...prev, author: value }))}
                        />
                    </div>

                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Publisher
                        </label>
                        <Dropdown
                            options={resources.publishers?.map(publisher => ({ label: publisher, value: publisher }))}
                            selected={formData.publisher}
                            onChange={value => setFormData(prev => ({ ...prev, publisher: value }))}
                        />
                    </div>
                </div>

                <div className="flex gap-2">
                    <button
                        type="submit"
                        disabled={loading}
                        className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                    >
                        {loading ? 'Saving...' : (book ? 'Update Book' : 'Add Book')}
                    </button>
                    <button
                        type="button"
                        onClick={onCancel}
                        className="px-4 py-2 bg-gray-300 text-gray-700 rounded-md hover:bg-gray-400 transition-colors"
                    >
                        Cancel
                    </button>
                </div>
            </form>
        </div>
    );
};

export default BookForm;