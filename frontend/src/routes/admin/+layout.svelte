<script lang="ts">
  import { onMount } from 'svelte';
  import { page } from '$app/stores';
  import { authStore, authActions } from '$lib/stores/auth';
  import { api } from '$lib/services/api';
  import { goto } from '$app/navigation';
  import AdminSidebar from '$lib/components/admin/AdminSidebar.svelte';
  import AdminHeader from '$lib/components/admin/AdminHeader.svelte';

  let sidebarOpen = false;
  let isLoading = true;

  $: isLoginPage = $page.url.pathname === '/admin/login';

  onMount(async () => {
    if (isLoginPage) {
      isLoading = false;
      return;
    }

    // Check if user is authenticated
    if (!$authStore.isAuthenticated) {
      goto('/admin/login');
      return;
    }

    try {
      // Verify token with backend
      await api.getCurrentUser();
      isLoading = false;
    } catch (error) {
      console.error('Auth verification failed:', error);
      authActions.clearAuth();
      goto('/admin/login');
    }
  });

  const toggleSidebar = () => {
    sidebarOpen = !sidebarOpen;
  };
</script>

<svelte:head>
  <title>Admin Panel - Brand IT Solution</title>
</svelte:head>

{#if isLoading}
  <div class="min-h-screen flex items-center justify-center bg-gray-100">
    <div class="text-center">
      <div class="spinner mx-auto mb-4"></div>
      <p class="text-gray-600">Loading...</p>
    </div>
  </div>
{:else if isLoginPage}
  <div class="min-h-screen bg-gradient-to-br from-primary-500 to-secondary-500">
    <slot />
  </div>
{:else}
  <div class="flex h-screen bg-gray-100">
    <!-- Sidebar -->
    <AdminSidebar bind:isOpen={sidebarOpen} />
    
    <!-- Main Content -->
    <div class="flex-1 flex flex-col overflow-hidden">
      <!-- Header -->
      <AdminHeader {toggleSidebar} />
      
      <!-- Page Content -->
      <main class="flex-1 overflow-x-hidden overflow-y-auto bg-gray-100 p-6">
        <slot />
      </main>
    </div>
  </div>

  <!-- Mobile sidebar overlay -->
  {#if sidebarOpen}
    <div 
      class="fixed inset-0 z-40 bg-black bg-opacity-50 lg:hidden"
      on:click={toggleSidebar}
      on:keydown={(e) => e.key === 'Escape' && toggleSidebar()}
      role="button"
      tabindex="0"
      aria-label="Close sidebar"
    ></div>
  {/if}
{/if}

<style>
  :global(.admin-layout) {
    font-family: 'Inter', 'Poppins', system-ui, sans-serif;
  }
</style>
