/**
 * API service to interact with the backend endpoints
 */
import {
    ApiResponse,
    Configuration,
    ConfigurationRequest,
    Intersection,
    IntersectionRequest,
    TrafficLight,
    TrafficLightRequest,
    TrafficDataRequest,
    TrafficDataResponse,
} from './api-types';

const API_BASE_URL = 'https://localhost:7161';

/**
 * Generic fetch wrapper with error handling
 */
async function fetchApi<T>(
    endpoint: string,
    options: RequestInit = {}
): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;

    const headers = {
        'Content-Type': 'application/json',
        ...options.headers,
    };

    const response = await fetch(url, {
        ...options,
        headers,
    });

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.errorMessage || 'An error occurred');
    }

    return response.json();
}

/**
 * Configuration API
 */
export const configurationApi = {
    getAll: () =>
        fetchApi<ApiResponse<Configuration[]>>('/api/v1/Configuration'),

    getById: (id: number) =>
        fetchApi<ApiResponse<Configuration>>(`/api/v1/Configuration/${id}`),

    create: (data: ConfigurationRequest) =>
        fetchApi<ApiResponse<Configuration>>('/api/v1/Configuration', {
            method: 'POST',
            body: JSON.stringify(data),
        }),

    update: (id: number, data: ConfigurationRequest) =>
        fetchApi<ApiResponse<Configuration>>(`/api/v1/Configuration/${id}`, {
            method: 'PUT',
            body: JSON.stringify(data),
        }),

    delete: (id: number) =>
        fetchApi<ApiResponse<Configuration>>(`/api/v1/Configuration/${id}`, {
            method: 'DELETE',
        }),
};

/**
 * Intersection API
 */
export const intersectionApi = {
    getAll: () =>
        fetchApi<ApiResponse<Intersection[]>>('/api/v1/Intersection'),

    getById: (id: number) =>
        fetchApi<ApiResponse<Intersection>>(`/api/v1/Intersection/${id}`),

    create: (data: IntersectionRequest) =>
        fetchApi<ApiResponse<Intersection>>('/api/v1/Intersection', {
            method: 'POST',
            body: JSON.stringify(data),
        }),

    update: (id: number, data: IntersectionRequest) =>
        fetchApi<ApiResponse<Intersection>>(`/api/v1/Intersection/${id}`, {
            method: 'PUT',
            body: JSON.stringify(data),
        }),

    delete: (id: number) =>
        fetchApi<ApiResponse<Intersection>>(`/api/v1/Intersection/${id}`, {
            method: 'DELETE',
        }),
};

/**
 * Traffic Light API
 */
export const trafficLightApi = {
    getAll: () =>
        fetchApi<ApiResponse<TrafficLight[]>>('/api/v1/TrafficLight'),

    getById: (id: number) =>
        fetchApi<ApiResponse<TrafficLight>>(`/api/v1/TrafficLight/${id}`),

    create: (data: TrafficLightRequest) =>
        fetchApi<ApiResponse<TrafficLight>>('/api/v1/TrafficLight', {
            method: 'POST',
            body: JSON.stringify(data),
        }),

    update: (id: number, data: TrafficLightRequest) =>
        fetchApi<ApiResponse<TrafficLight>>(`/api/v1/TrafficLight/${id}`, {
            method: 'PUT',
            body: JSON.stringify(data),
        }),

    delete: (id: number) =>
        fetchApi<ApiResponse<TrafficLight>>(`/api/v1/TrafficLight/${id}`, {
            method: 'DELETE',
        }),
};

/**
 * Traffic Flow API
 */
export const trafficFlowApi = {
    calculateGreenLight: (data: TrafficDataRequest) =>
        fetchApi<ApiResponse<TrafficDataResponse>>('/api/v1/TrafficFlow/calculate-green-light', {
            method: 'POST',
            body: JSON.stringify(data),
        }),
};