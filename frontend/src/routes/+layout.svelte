<script lang="ts">
  import '../app.css';
  import Header from '$lib/components/Header.svelte';
  import Footer from '$lib/components/Footer.svelte';
  import { page } from '$app/stores';
  import { onMount } from 'svelte';
  import { themeActions } from '$lib/stores/theme';
  
  onMount(() => {
    // Initialize theme
    themeActions.init();
    
    // Add smooth scrolling behavior
    document.documentElement.style.scrollBehavior = 'smooth';
  });
</script>

<svelte:head>
  <title>Brand IT Solution - Professional IT Services & Digital Solutions</title>
</svelte:head>

<div class="min-h-screen flex flex-col bg-white dark:bg-gray-900 text-gray-900 dark:text-white transition-colors duration-300">
  <Header />
  
  <main class="flex-grow">
    <slot />
  </main>
  
  <Footer />
</div>

<!-- Background Decorative Elements -->
<div class="fixed inset-0 -z-10 overflow-hidden pointer-events-none">
  <div class="absolute top-0 -left-4 w-72 h-72 bg-purple-300 dark:bg-purple-600 rounded-full mix-blend-multiply dark:mix-blend-soft-light filter blur-xl opacity-70 dark:opacity-30 animate-float transition-opacity duration-300"></div>
  <div class="absolute top-0 -right-4 w-72 h-72 bg-yellow-300 dark:bg-indigo-600 rounded-full mix-blend-multiply dark:mix-blend-soft-light filter blur-xl opacity-70 dark:opacity-30 animate-float transition-opacity duration-300" style="animation-delay: 2s;"></div>
  <div class="absolute -bottom-8 left-20 w-72 h-72 bg-pink-300 dark:bg-pink-600 rounded-full mix-blend-multiply dark:mix-blend-soft-light filter blur-xl opacity-70 dark:opacity-30 animate-float transition-opacity duration-300" style="animation-delay: 4s;"></div>
</div>

<style>
  :global(body) {
    font-family: 'Inter', 'Poppins', system-ui, sans-serif;
  }
</style>
