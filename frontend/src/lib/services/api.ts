import { authStore, authActions } from '$lib/stores/auth';
import { get } from 'svelte/store';
import { browser } from '$app/environment';

const API_BASE_URL = 'http://localhost:5000/api';

class ApiService {
  private getAuthHeaders(): HeadersInit {
    const auth = get(authStore);
    const headers: HeadersInit = {
      'Content-Type': 'application/json'
    };

    if (auth.token) {
      headers['Authorization'] = `Bearer ${auth.token}`;
    }

    return headers;
  }

  private async handleResponse<T>(response: Response): Promise<T> {
    if (response.status === 401) {
      // Token expired or invalid
      authActions.clearAuth();
      if (browser) {
        window.location.href = '/admin/login';
      }
      throw new Error('Unauthorized');
    }

    if (!response.ok) {
      const error = await response.json().catch(() => ({ message: 'An error occurred' }));
      throw new Error(error.message || 'An error occurred');
    }

    return response.json();
  }

  async request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;
    const config: RequestInit = {
      ...options,
      headers: {
        ...this.getAuthHeaders(),
        ...options.headers
      }
    };

    const response = await fetch(url, config);
    return this.handleResponse<T>(response);
  }

  // Auth endpoints
  async login(email: string, password: string) {
    return this.request<any>('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, password })
    });
  }

  async logout() {
    return this.request<any>('/auth/logout', {
      method: 'POST'
    });
  }

  async getCurrentUser() {
    return this.request<any>('/auth/me');
  }

  // Services endpoints
  async getServices() {
    return this.request<any[]>('/services');
  }

  async getService(id: string) {
    return this.request<any>(`/services/${id}`);
  }

  async createService(service: any) {
    return this.request<any>('/services', {
      method: 'POST',
      body: JSON.stringify(service)
    });
  }

  async updateService(id: string, service: any) {
    return this.request<any>(`/services/${id}`, {
      method: 'PUT',
      body: JSON.stringify(service)
    });
  }

  async deleteService(id: string) {
    return this.request<void>(`/services/${id}`, {
      method: 'DELETE'
    });
  }

  // Contact endpoints
  async getContactMessages(params?: { status?: string; priority?: string; page?: number; pageSize?: number }) {
    const query = new URLSearchParams();
    if (params) {
      Object.entries(params).forEach(([key, value]) => {
        if (value !== undefined) {
          query.append(key, value.toString());
        }
      });
    }
    
    const endpoint = `/contact${query.toString() ? `?${query.toString()}` : ''}`;
    return this.request<any[]>(endpoint);
  }

  async getContactMessage(id: string) {
    return this.request<any>(`/contact/${id}`);
  }

  async updateContactMessage(id: string, message: any) {
    return this.request<any>(`/contact/${id}`, {
      method: 'PUT',
      body: JSON.stringify(message)
    });
  }

  async deleteContactMessage(id: string) {
    return this.request<void>(`/contact/${id}`, {
      method: 'DELETE'
    });
  }

  async getContactStats() {
    return this.request<any>('/contact/stats');
  }

  // Public contact form
  async submitContactMessage(message: any) {
    return this.request<any>('/contact', {
      method: 'POST',
      body: JSON.stringify(message)
    });
  }
}

export const api = new ApiService();
