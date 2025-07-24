import config from '../config/settings';

const API_BASE_URL = config.apiSettings.libraryUrl;

const resourcesService = {
    getResources: async () => {
        try {
            const response = await fetch(`${API_BASE_URL}/resources`);
            if (!response.ok) throw new Error('Failed to fetch resources');
            return await response.json();
        } catch (error) {
            console.error('Error fetching resources:', error);
            throw error;
        }
    }
}

export default resourcesService;