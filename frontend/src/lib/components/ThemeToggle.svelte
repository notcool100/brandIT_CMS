<script lang="ts">
  import { onMount } from 'svelte';
  import { theme, themeActions } from '$lib/stores/theme';
  import { Sun, Moon } from 'lucide-svelte';

  let mounted = false;

  onMount(() => {
    themeActions.init();
    mounted = true;
  });
</script>

{#if mounted}
  <div class="theme-toggle-container">
    <button
      on:click={themeActions.toggle}
      class="theme-toggle-btn group relative w-16 h-8 bg-gray-200 dark:bg-gray-700 rounded-full p-1 transition-all duration-300 ease-in-out focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2 hover:shadow-lg"
      aria-label="Toggle theme"
    >
      <!-- Toggle Track -->
      <div class="absolute inset-1 bg-gradient-to-r from-yellow-300 to-orange-400 dark:from-indigo-500 dark:to-purple-600 rounded-full opacity-0 group-hover:opacity-20 transition-opacity duration-300"></div>
      
      <!-- Toggle Ball -->
      <div class="toggle-ball relative w-6 h-6 bg-white rounded-full shadow-lg transform transition-all duration-300 ease-in-out {$theme === 'dark' ? 'translate-x-8' : 'translate-x-0'} flex items-center justify-center">
        <!-- Sun Icon -->
        <div class="absolute inset-0 flex items-center justify-center transition-all duration-300 {$theme === 'dark' ? 'opacity-0 scale-0 rotate-90' : 'opacity-100 scale-100 rotate-0'}">
          <Sun size={14} class="text-yellow-500" />
        </div>
        
        <!-- Moon Icon -->
        <div class="absolute inset-0 flex items-center justify-center transition-all duration-300 {$theme === 'dark' ? 'opacity-100 scale-100 rotate-0' : 'opacity-0 scale-0 -rotate-90'}">
          <Moon size={14} class="text-indigo-600" />
        </div>
      </div>

      <!-- Day/Night Background Effects -->
      <div class="absolute inset-0 rounded-full overflow-hidden pointer-events-none">
        <!-- Day Sky -->
        <div class="absolute inset-0 bg-gradient-to-r from-blue-400 via-blue-300 to-yellow-300 opacity-0 {$theme === 'light' ? 'opacity-100' : 'opacity-0'} transition-opacity duration-500"></div>
        
        <!-- Night Sky -->
        <div class="absolute inset-0 bg-gradient-to-r from-indigo-900 via-purple-800 to-indigo-900 opacity-0 {$theme === 'dark' ? 'opacity-100' : 'opacity-0'} transition-opacity duration-500"></div>
        
        <!-- Stars (only visible in dark mode) -->
        {#if $theme === 'dark'}
          <div class="absolute inset-0">
            <div class="absolute top-2 left-3 w-0.5 h-0.5 bg-white rounded-full animate-sparkle"></div>
            <div class="absolute top-3 right-4 w-0.5 h-0.5 bg-white rounded-full animate-sparkle" style="animation-delay: 0.5s;"></div>
            <div class="absolute bottom-2 left-5 w-0.5 h-0.5 bg-white rounded-full animate-sparkle" style="animation-delay: 1s;"></div>
          </div>
        {/if}
      </div>
    </button>

    <!-- Theme Labels -->
    <div class="flex items-center justify-between mt-2 text-xs text-gray-500 dark:text-gray-400">
      <span class="{$theme === 'light' ? 'text-yellow-600 font-medium' : ''} transition-colors duration-300">Day</span>
      <span class="{$theme === 'dark' ? 'text-indigo-400 font-medium' : ''} transition-colors duration-300">Night</span>
    </div>
  </div>
{:else}
  <!-- Loading state -->
  <div class="w-16 h-8 bg-gray-200 rounded-full animate-pulse"></div>
{/if}

<style>
  .theme-toggle-btn:hover .toggle-ball {
    transform: scale(1.1) translateX(var(--tw-translate-x));
  }

  .theme-toggle-container {
    user-select: none;
  }

  /* Custom sparkle animation for stars */
  @keyframes sparkle {
    0%, 100% {
      opacity: 0.3;
      transform: scale(1);
    }
    50% {
      opacity: 1;
      transform: scale(1.5);
    }
  }

  .animate-sparkle {
    animation: sparkle 2s ease-in-out infinite;
  }
</style>
