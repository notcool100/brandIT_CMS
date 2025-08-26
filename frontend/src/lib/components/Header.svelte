<script lang="ts">
  import { onMount } from 'svelte';
  import { Menu, X, ChevronDown } from 'lucide-svelte';
  import ThemeToggle from './ThemeToggle.svelte';

  let isMenuOpen = false;
  let isScrolled = false;
  let isServicesOpen = false;

  const toggleMenu = () => {
    isMenuOpen = !isMenuOpen;
  };

  const scrollToSection = (sectionId: string) => {
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
    isMenuOpen = false;
  };

  onMount(() => {
    const handleScroll = () => {
      isScrolled = window.scrollY > 50;
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  });

  const navItems = [
    { label: 'Home', href: '#home' },
    { label: 'About', href: '#about' },
    { 
      label: 'Services', 
      href: '#services',
      dropdown: [
        { label: 'Web Development', href: '#services' },
        { label: 'Cloud Solutions', href: '#services' },
        { label: 'IT Consulting', href: '#services' },
        { label: 'Digital Marketing', href: '#services' }
      ]
    },
    { label: 'Team', href: '#team' },
    { label: 'Blog', href: '#blog' },
    { label: 'Contact', href: '#contact' }
  ];
</script>

<header 
  class="fixed top-0 left-0 right-0 z-50 transition-all duration-300 ease-in-out {isScrolled ? 'bg-white/95 dark:bg-gray-900/95 backdrop-blur-md shadow-lg' : 'bg-transparent'}"
>
  <nav class="container-custom">
    <div class="flex items-center justify-between h-16 md:h-20">
      <!-- Logo -->
      <div class="flex items-center">
        <a href="/" class="flex items-center space-x-2 group">
          <div class="w-10 h-10 bg-gradient-to-br from-primary-500 to-primary-700 rounded-lg flex items-center justify-center transform group-hover:scale-110 transition-transform duration-300">
            <span class="text-white font-bold text-xl">B</span>
          </div>
          <span class="text-xl md:text-2xl font-bold font-heading {isScrolled ? 'text-gray-900 dark:text-white' : 'text-white'} group-hover:text-primary-600 transition-colors duration-300">
            Brand IT Solution
          </span>
        </a>
      </div>

      <!-- Desktop Navigation -->
      <div class="hidden lg:flex items-center space-x-8">
        {#each navItems as item}
          <div class="relative group">
            {#if item.dropdown}
              <button 
                class="flex items-center space-x-1 {isScrolled ? 'text-gray-700 dark:text-gray-300 hover:text-primary-600 dark:hover:text-primary-400' : 'text-white hover:text-primary-200'} font-medium transition-colors duration-300"
                on:mouseenter={() => isServicesOpen = true}
                on:mouseleave={() => isServicesOpen = false}
              >
                <span>{item.label}</span>
                <ChevronDown size={16} class="transform group-hover:rotate-180 transition-transform duration-300" />
              </button>
              
              <!-- Dropdown Menu -->
              <div 
                role="menu"
                tabindex="-1"
                class="absolute top-full left-0 mt-2 w-56 bg-white rounded-lg shadow-xl border border-gray-100 opacity-0 invisible group-hover:opacity-100 group-hover:visible transform translate-y-2 group-hover:translate-y-0 transition-all duration-300 ease-out"
                on:mouseenter={() => isServicesOpen = true}
                on:mouseleave={() => isServicesOpen = false}
              >
                {#each item.dropdown as dropItem}
                  <button
                    on:click={() => scrollToSection(dropItem.href.slice(1))}
                    class="block w-full text-left px-4 py-3 text-gray-700 hover:text-primary-600 hover:bg-primary-50 transition-colors duration-200 first:rounded-t-lg last:rounded-b-lg"
                  >
                    {dropItem.label}
                  </button>
                {/each}
              </div>
            {:else}
              <button
                on:click={() => scrollToSection(item.href.slice(1))}
                class="{isScrolled ? 'text-gray-700 hover:text-primary-600' : 'text-white hover:text-primary-200'} font-medium transition-colors duration-300 relative group"
              >
                {item.label}
                <span class="absolute -bottom-1 left-0 w-0 h-0.5 bg-primary-500 group-hover:w-full transition-all duration-300"></span>
              </button>
            {/if}
          </div>
        {/each}
      </div>

      <!-- Theme Toggle & CTA Button -->
      <div class="hidden lg:flex items-center space-x-6">
        <ThemeToggle />
        <button
          on:click={() => scrollToSection('contact')}
          class="btn-primary"
        >
          Get Started
        </button>
      </div>

      <!-- Mobile Menu Button -->
      <button
        on:click={toggleMenu}
        class="lg:hidden {isScrolled ? 'text-gray-700 dark:text-gray-300' : 'text-white'} hover:text-primary-600 transition-colors duration-300"
        aria-label="Toggle menu"
      >
        {#if isMenuOpen}
          <X size={24} />
        {:else}
          <Menu size={24} />
        {/if}
      </button>
    </div>

    <!-- Mobile Navigation -->
    <div class="lg:hidden {isMenuOpen ? 'block' : 'hidden'}">
      <div class="px-2 pt-2 pb-3 space-y-1 bg-white/95 backdrop-blur-md rounded-lg mt-2 shadow-lg border border-gray-100">
        {#each navItems as item}
          {#if item.dropdown}
            <div class="space-y-1">
              <button
                class="block w-full text-left px-3 py-2 text-gray-700 hover:text-primary-600 hover:bg-primary-50 rounded-md transition-colors duration-200 font-medium"
                on:click={() => isServicesOpen = !isServicesOpen}
              >
                <div class="flex items-center justify-between">
                  <span>{item.label}</span>
                  <ChevronDown size={16} class="transform {isServicesOpen ? 'rotate-180' : ''} transition-transform duration-300" />
                </div>
              </button>
              {#if isServicesOpen}
                <div class="pl-4 space-y-1">
                  {#each item.dropdown as dropItem}
                    <button
                      on:click={() => scrollToSection(dropItem.href.slice(1))}
                      class="block w-full text-left px-3 py-2 text-gray-600 hover:text-primary-600 hover:bg-primary-50 rounded-md transition-colors duration-200"
                    >
                      {dropItem.label}
                    </button>
                  {/each}
                </div>
              {/if}
            </div>
          {:else}
            <button
              on:click={() => scrollToSection(item.href.slice(1))}
              class="block w-full text-left px-3 py-2 text-gray-700 hover:text-primary-600 hover:bg-primary-50 rounded-md transition-colors duration-200 font-medium"
            >
              {item.label}
            </button>
          {/if}
        {/each}
        
        <div class="pt-4 border-t border-gray-200 space-y-4">
          <div class="flex justify-center">
            <ThemeToggle />
          </div>
          <button
            on:click={() => scrollToSection('contact')}
            class="w-full btn-primary text-center"
          >
            Get Started
          </button>
        </div>
      </div>
    </div>
  </nav>
</header>
