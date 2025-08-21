<script lang="ts">
  import { onMount } from 'svelte';
  
  // Import components directly for faster loading
  import Hero from '$lib/components/Hero.svelte';
  import Services from '$lib/components/Services.svelte';
  import About from '$lib/components/About.svelte';
  import Team from '$lib/components/Team.svelte';
  import Testimonials from '$lib/components/Testimonials.svelte';
  import Blog from '$lib/components/Blog.svelte';
  import Contact from '$lib/components/Contact.svelte';
  import Stats from '$lib/components/Stats.svelte';
  
  let isLoaded = false;
  let showLoading = true;

  onMount(() => {
    // Quick loading simulation for smooth UX
    setTimeout(() => {
      isLoaded = true;
      showLoading = false;
    }, 800); // Reduced from longer wait time

    // Enhanced intersection observer with stagger animations
    const observerOptions = {
      threshold: 0.05,
      rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('animate-slideInUp');
          
          // Add stagger animation to child elements
          const children = entry.target.querySelectorAll('.stagger-item');
          children.forEach((child, index) => {
            setTimeout(() => {
              child.classList.add('animate-fadeInScale');
            }, index * 80); // Faster stagger
          });
        }
      });
    }, observerOptions);

    // Small delay to let components render
    setTimeout(() => {
      const sections = document.querySelectorAll('section');
      sections.forEach((section) => {
        observer.observe(section);
      });
    }, 100);

    return () => {
      observer.disconnect();
    };
  });
</script>

<svelte:head>
  <title>Brand IT Solution - Professional IT Services & Digital Solutions</title>
  <meta name="description" content="Transform your business with Brand IT Solution's cutting-edge IT services, web development, cloud solutions, and digital transformation expertise." />
</svelte:head>

<!-- Always show content with optional loading overlay -->
<div class="relative">
  <!-- Page Content - Always loaded -->
  <div class="{isLoaded ? 'animate-fadeInPage' : 'opacity-90'}">
    <Hero />
    <Stats />
    <section id="about" class="scroll-reveal">
      <About />
    </section>
    <section id="services" class="scroll-reveal">
      <Services />
    </section>
    <section id="team" class="scroll-reveal">
      <Team />
    </section>
    <section id="testimonials" class="scroll-reveal">
      <Testimonials />
    </section>
    <section id="blog" class="scroll-reveal">
      <Blog />
    </section>
    <section id="contact" class="scroll-reveal">
      <Contact />
    </section>
  </div>

  <!-- Optional Loading Overlay (only for first 800ms) -->
  {#if showLoading}
    <div class="fixed inset-0 z-50 flex items-center justify-center bg-gradient-to-br from-primary-600 via-primary-700 to-secondary-600 transition-opacity duration-500">
      <div class="text-center space-y-6">
        <!-- Simplified Animated Logo -->
        <div class="relative">
          <div class="w-20 h-20 bg-white/20 rounded-2xl flex items-center justify-center mx-auto animate-pulse backdrop-blur-sm border border-white/30">
            <span class="text-white text-3xl font-bold">B</span>
          </div>
          <!-- Simple rotating border -->
          <div class="absolute inset-0 border-2 border-white/30 border-t-white rounded-2xl animate-spin"></div>
        </div>
        
        <!-- Fast Loading Text -->
        <div class="space-y-3">
          <h2 class="text-xl font-bold text-white">Brand IT Solution</h2>
          <div class="flex items-center justify-center space-x-1">
            <div class="w-1.5 h-1.5 bg-white rounded-full animate-bounce" style="animation-delay: 0ms;"></div>
            <div class="w-1.5 h-1.5 bg-white rounded-full animate-bounce" style="animation-delay: 100ms;"></div>
            <div class="w-1.5 h-1.5 bg-white rounded-full animate-bounce" style="animation-delay: 200ms;"></div>
          </div>
        </div>
      </div>
    </div>
  {/if}
</div>
