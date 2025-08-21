import { writable } from 'svelte/stores';
import { browser } from '$app/environment';

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  fullName: string;
  role: string;
  isActive: boolean;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  refreshToken: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
}

const initialState: AuthState = {
  user: null,
  token: null,
  refreshToken: null,
  isAuthenticated: false,
  isLoading: false
};

// Load from localStorage if available
const loadFromStorage = (): AuthState => {
  if (!browser) return initialState;
  
  try {
    const stored = localStorage.getItem('auth');
    if (stored) {
      const parsed = JSON.parse(stored);
      return {
        ...parsed,
        isLoading: false
      };
    }
  } catch (error) {
    console.error('Error loading auth from storage:', error);
    localStorage.removeItem('auth');
  }
  
  return initialState;
};

export const authStore = writable<AuthState>(loadFromStorage());

// Subscribe to store changes and persist to localStorage
if (browser) {
  authStore.subscribe((state) => {
    if (state.isAuthenticated && state.user && state.token) {
      localStorage.setItem('auth', JSON.stringify({
        user: state.user,
        token: state.token,
        refreshToken: state.refreshToken,
        isAuthenticated: state.isAuthenticated
      }));
    } else {
      localStorage.removeItem('auth');
    }
  });
}

export const authActions = {
  setLoading: (loading: boolean) => {
    authStore.update(state => ({ ...state, isLoading: loading }));
  },

  setAuth: (user: User, token: string, refreshToken: string) => {
    authStore.update(state => ({
      ...state,
      user,
      token,
      refreshToken,
      isAuthenticated: true,
      isLoading: false
    }));
  },

  clearAuth: () => {
    authStore.set(initialState);
    if (browser) {
      localStorage.removeItem('auth');
    }
  },

  updateUser: (user: Partial<User>) => {
    authStore.update(state => ({
      ...state,
      user: state.user ? { ...state.user, ...user } : null
    }));
  }
};
