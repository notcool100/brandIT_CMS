<script lang="ts">
  import { onMount } from 'svelte';
  import { goto } from '$app/navigation';
  import { authStore, authActions } from '$lib/stores/auth';
  import { api } from '$lib/services/api';
  import { Mail, Lock, Eye, EyeOff, AlertCircle } from 'lucide-svelte';

  let email = '';
  let password = '';
  let showPassword = false;
  let isLoading = false;
  let error = '';

  onMount(() => {
    // Redirect if already authenticated
    if ($authStore.isAuthenticated) {
      goto('/admin');
    }
  });

  const handleSubmit = async (e: Event) => {
    e.preventDefault();
    
    if (!email || !password) {
      error = 'Please fill in all fields';
      return;
    }

    isLoading = true;
    error = '';

    try {
      const response = await api.login(email, password);
      
      authActions.setAuth(
        response.User,
        response.AccessToken,
        response.RefreshToken
      );

      goto('/admin');
    } catch (err: any) {
      error = err.message || 'Login failed. Please try again.';
    } finally {
      isLoading = false;
    }
  };

  const togglePasswordVisibility = () => {
    showPassword = !showPassword;
  };
</script>

<div class="min-h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
  <div class="max-w-md w-full space-y-8">
    <!-- Header -->
    <div class="text-center">
      <div class="mx-auto w-16 h-16 bg-white rounded-xl shadow-lg flex items-center justify-center mb-6">
        <span class="text-2xl font-bold text-primary-600">B</span>
      </div>
      <h2 class="text-3xl font-bold text-white mb-2">
        Admin Login
      </h2>
      <p class="text-blue-100">
        Sign in to access the admin dashboard
      </p>
    </div>

    <!-- Login Form -->
    <div class="bg-white/10 backdrop-blur-md rounded-2xl shadow-2xl p-8 border border-white/20">
      <form on:submit={handleSubmit} class="space-y-6">
        <!-- Error Message -->
        {#if error}
          <div class="bg-red-500/20 border border-red-500/30 rounded-lg p-4 flex items-center space-x-3">
            <AlertCircle size={20} class="text-red-300" />
            <span class="text-red-200">{error}</span>
          </div>
        {/if}

        <!-- Email Field -->
        <div>
          <label for="email" class="block text-sm font-semibold text-white mb-2">
            Email Address
          </label>
          <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Mail size={20} class="text-gray-300" />
            </div>
            <input
              id="email"
              type="email"
              bind:value={email}
              required
              class="w-full pl-10 pr-4 py-3 bg-white/10 border border-white/20 rounded-lg text-white placeholder-gray-300 focus:ring-2 focus:ring-white/50 focus:border-transparent transition-all duration-300"
              placeholder="admin@brandit.com"
            />
          </div>
        </div>

        <!-- Password Field -->
        <div>
          <label for="password" class="block text-sm font-semibold text-white mb-2">
            Password
          </label>
          <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Lock size={20} class="text-gray-300" />
            </div>
            {#if showPassword}
              <input
                id="password"
                type="text"
                bind:value={password}
                required
                class="w-full pl-10 pr-12 py-3 bg-white/10 border border-white/20 rounded-lg text-white placeholder-gray-300 focus:ring-2 focus:ring-white/50 focus:border-transparent transition-all duration-300"
                placeholder="Enter your password"
              />
            {:else}
              <input
                id="password"
                type="password"
                bind:value={password}
                required
                class="w-full pl-10 pr-12 py-3 bg-white/10 border border-white/20 rounded-lg text-white placeholder-gray-300 focus:ring-2 focus:ring-white/50 focus:border-transparent transition-all duration-300"
                placeholder="Enter your password"
              />
            {/if}
            <button
              type="button"
              on:click={togglePasswordVisibility}
              class="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-300 hover:text-white transition-colors duration-300"
            >
              {#if showPassword}
                <EyeOff size={20} />
              {:else}
                <Eye size={20} />
              {/if}
            </button>
          </div>
        </div>

        <!-- Submit Button -->
        <button
          type="submit"
          disabled={isLoading}
          class="w-full bg-white text-primary-700 hover:bg-gray-100 disabled:bg-gray-300 disabled:text-gray-500 font-semibold py-3 px-4 rounded-lg shadow-lg hover:shadow-xl transform hover:-translate-y-1 disabled:hover:translate-y-0 transition-all duration-300 ease-out flex items-center justify-center space-x-2"
        >
          {#if isLoading}
            <div class="spinner"></div>
            <span>Signing In...</span>
          {:else}
            <span>Sign In</span>
          {/if}
        </button>
      </form>

      <!-- Demo Credentials -->
      <div class="mt-8 p-4 bg-white/5 rounded-lg border border-white/10">
        <h4 class="text-white font-semibold mb-2">Demo Credentials:</h4>
        <p class="text-blue-200 text-sm">
          <strong>Email:</strong> admin@brandit.com<br>
          <strong>Password:</strong> Admin@123
        </p>
      </div>
    </div>

    <!-- Back to Site -->
    <div class="text-center">
      <a 
        href="/" 
        class="text-blue-200 hover:text-white transition-colors duration-300 text-sm"
      >
        ← Back to website
      </a>
    </div>
  </div>
</div>
