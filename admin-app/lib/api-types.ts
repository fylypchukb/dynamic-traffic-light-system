/**
 * Type definitions derived from the OpenAPI specification
 */

// Common API response wrapper
export interface ApiResponse<T> {
    result: T;
    errorMessage: string | null;
    hasError: boolean;
}

// Configuration Types
export interface ConfigurationRequest {
    trafficLightId: number;
    minGreenTime: number;
    maxGreenTime: number;
    timePerVehicle: number;
    sequenceGreenTime?: Record<string, number | null>;
    defaultGreenTime: number;
    defaultRedTime: number;
    isActive: boolean;
}

export interface Configuration extends ConfigurationRequest {
    id: number;
    createTime: string;
    createdByName: string | null;
    lastUpdateTime: string;
    lastUpdatedByName: string | null;
}

// Intersection Types
export interface IntersectionRequest {
    city: string | null;
    location: string | null;
    isActive: boolean;
}

export interface Intersection extends IntersectionRequest {
    id: number;
    createTime: string;
    createdByName: string | null;
    lastUpdateTime: string;
    lastUpdatedByName: string | null;
}

// Traffic Light Types
export interface TrafficLightRequest {
    name: string | null;
    intersectionId: number;
    priority: number;
    isActive: boolean;
}

export interface TrafficLight extends TrafficLightRequest {
    id: number;
    createTime: string;
    createdByName: string | null;
    lastUpdateTime: string;
    lastUpdatedByName: string | null;
}

// Traffic Flow Types
export interface TrafficDataRequest {
    trafficLightId: number;
    carsNumber: number;
}

export interface TrafficDataResponse {
    trafficLightId: number;
    greenLightDuration: number;
}