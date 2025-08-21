<script lang="ts">
  import { authStore } from '$lib/stores/auth';
  import { Menu, Bell, User, Settings, LogOut } from 'lucide-svelte';
  import { authActions } from '$lib/stores/auth';
  import { goto } from '$app/navigation';

  export let toggleSidebar: () => void;

  let showUserMenu = false;

  const handleLogout = async () => {
    authActions.clearAuth();
    goto('/admin/login');
  };

  const toggleUserMenu = () => {
    showUserMenu = !showUserMenu;
  };

  // Close user menu when clicking outside
  const handleClickOutside = (event: MouseEvent) => {
    const target = event.target as HTMLElement;
    if (!target.closest('.user-menu-container')) {
      showUserMenu = false;
    }
  };

  $: if (typeof window !== 'undefined') {
    if (showUserMenu) {
      document.addEventListener('click', handleClickOutside);
    } else {
      document.removeEventListener('click', handleClickOutside);
    }
  }
</script>

<header class="bg-white shadow-sm border-b border-gray-200 h-16 flex items-center justify-between px-6">
  <!-- Left side -->
  <div class="flex items-center space-x-4">
    <!-- Mobile menu button -->
    <button
      on:click={toggleSidebar}
      class="lg:hidden p-2 rounded-lg hover:bg-gray-100 transition-colors duration-200"
      aria-label="Toggle sidebar"
    >
      <Menu size={20} class="text-gray-600" />
    </button>

    <!-- Page title will be set by individual pages -->
    <div class="hidden lg:block">
      <h1 class="text-lg font-semibold text-gray-900">
        <!-- This will be filled by page components -->
      </h1>
    </div>
  </div>

  <!-- Right side -->
  <div class="flex items-center space-x-4">
    <!-- Notifications -->
    <button class="relative p-2 rounded-lg hover:bg-gray-100 transition-colors duration-200">
      <Bell size={20} class="text-gray-600" />
      <!-- Notification badge -->
      <span class="absolute -top-1 -right-1 w-3 h-3 bg-red-500 rounded-full"></span>
    </button>

    <!-- User menu -->
    <div class="relative user-menu-container">
      <button
        on:click={toggleUserMenu}
        class="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-100 transition-colors duration-200"
      >
        <div class="w-8 h-8 bg-gradient-to-br from-primary-400 to-secondary-400 rounded-full flex items-center justify-center">
          <span class="text-white text-sm font-semibold">
            {$authStore.user?.firstName?.[0] || 'A'}
          </span>
        </div>
        <div class="hidden lg:block text-left">
          <div class="text-sm font-medium text-gray-900">
            {$authStore.user?.fullName || 'Admin User'}
          </div>
          <div class="text-xs text-gray-500">
            {$authStore.user?.role || 'Administrator'}
          </div>
        </div>
      </button>

      <!-- User dropdown menu -->
      {#if showUserMenu}
        <div class="absolute right-0 mt-2 w-56 bg-white rounded-lg shadow-lg border border-gray-200 py-2 z-50">
          <!-- User info -->
          <div class="px-4 py-3 border-b border-gray-100">
            <div class="text-sm font-medium text-gray-900">
              {$authStore.user?.fullName || 'Admin User'}
            </div>
            <div class="text-sm text-gray-500">
              {$authStore.user?.email || 'admin@brandit.com'}
            </div>
          </div>

          <!-- Menu items -->
          <div class="py-2">
            <a
              href="/admin/profile"
              class="flex items-center space-x-2 px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 transition-colors duration-200"
            >
              <User size={16} />
              <span>Profile</span>
            </a>
            
            <a
              href="/admin/settings"
              class="flex items-center space-x-2 px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 transition-colors duration-200"
            >
              <Settings size={16} />
              <span>Settings</span>
            </a>
            
            <hr class="my-2 border-gray-100">
            
            <button
              on:click={handleLogout}
              class="flex items-center space-x-2 w-full px-4 py-2 text-sm text-red-600 hover:bg-red-50 transition-colors duration-200"
            >
              <LogOut size={16} />
              <span>Sign out</span>
            </button>
          </div>
        </div>
      {/if}
    </div>
  </div>
</header>
