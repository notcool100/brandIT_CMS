<script lang="ts">
  import { page } from '$app/stores';
  import { 
    LayoutDashboard, 
    Settings, 
    Users, 
    Mail, 
    FileText, 
    UserCheck,
    BarChart3,
    LogOut
  } from 'lucide-svelte';
  import { authActions } from '$lib/stores/auth';
  import { goto } from '$app/navigation';

  export let isOpen = false;

  const menuItems = [
    { icon: LayoutDashboard, label: 'Dashboard', href: '/admin', exact: true },
    { icon: Settings, label: 'Services', href: '/admin/services' },
    { icon: UserCheck, label: 'Team Members', href: '/admin/team' },
    { icon: FileText, label: 'Blog Posts', href: '/admin/blog' },
    { icon: Mail, label: 'Messages', href: '/admin/messages' },
    { icon: Users, label: 'Users', href: '/admin/users' },
    { icon: BarChart3, label: 'Analytics', href: '/admin/analytics' }
  ];

  $: currentPath = $page.url.pathname;

  const isActive = (href: string, exact = false) => {
    if (exact) {
      return currentPath === href;
    }
    return currentPath.startsWith(href);
  };

  const handleLogout = async () => {
    try {
      authActions.clearAuth();
      goto('/admin/login');
    } catch (error) {
      console.error('Logout error:', error);
      // Still clear auth and redirect even if API call fails
      authActions.clearAuth();
      goto('/admin/login');
    }
  };
</script>

<!-- Sidebar -->
<div class="bg-white shadow-lg {isOpen ? 'translate-x-0' : '-translate-x-full'} lg:translate-x-0 transition-transform duration-300 ease-in-out w-64 fixed lg:static inset-y-0 left-0 z-50">
  <div class="h-full flex flex-col">
    <!-- Logo -->
    <div class="flex items-center justify-center h-16 px-6 border-b border-gray-200">
      <div class="flex items-center space-x-3">
        <div class="w-8 h-8 bg-gradient-to-br from-primary-500 to-primary-700 rounded-lg flex items-center justify-center">
          <span class="text-white font-bold text-sm">B</span>
        </div>
        <div class="text-xl font-bold text-gray-900">Admin Panel</div>
      </div>
    </div>

    <!-- Navigation -->
    <nav class="flex-1 px-4 py-6 space-y-2">
      {#each menuItems as item}
        <a
          href={item.href}
          class="flex items-center space-x-3 px-3 py-2 rounded-lg text-sm font-medium transition-colors duration-200 {
            isActive(item.href, item.exact)
              ? 'bg-primary-100 text-primary-700 border-r-2 border-primary-500'
              : 'text-gray-600 hover:bg-gray-100 hover:text-gray-900'
          }"
        >
          <svelte:component this={item.icon} size={18} />
          <span>{item.label}</span>
        </a>
      {/each}
    </nav>

    <!-- Logout Button -->
    <div class="px-4 py-4 border-t border-gray-200">
      <button
        on:click={handleLogout}
        class="flex items-center space-x-3 w-full px-3 py-2 rounded-lg text-sm font-medium text-red-600 hover:bg-red-50 hover:text-red-700 transition-colors duration-200"
      >
        <LogOut size={18} />
        <span>Logout</span>
      </button>
    </div>

    <!-- Version Info -->
    <div class="px-4 py-2 border-t border-gray-200">
      <div class="text-xs text-gray-500 text-center">
        Brand IT Solution v1.0
      </div>
    </div>
  </div>
</div>
