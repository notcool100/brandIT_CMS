<script lang="ts">
  import { onMount } from 'svelte';
  import { ArrowRight, Play, Star, Users, Award, Zap } from 'lucide-svelte';

  let heroRef: HTMLElement;
  let isVisible = false;

  const scrollToContact = () => {
    const element = document.getElementById('contact');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  };

  const scrollToServices = () => {
    const element = document.getElementById('services');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  };

  onMount(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            isVisible = true;
          }
        });
      },
      { threshold: 0.1 }
    );

    if (heroRef) {
      observer.observe(heroRef);
    }

    return () => observer.disconnect();
  });

  // Floating elements data
  const floatingElements = [
    { icon: Star, delay: 0, position: 'top-20 left-10' },
    { icon: Users, delay: 1, position: 'top-32 right-20' },
    { icon: Award, delay: 2, position: 'bottom-40 left-20' },
    { icon: Zap, delay: 3, position: 'bottom-20 right-10' }
  ];
</script>

<section
  bind:this={heroRef}
  id="home"
  class="relative min-h-screen flex items-center justify-center overflow-hidden hero-bg"
>
  <!-- Animated Background -->
  <div class="absolute inset-0">
    <div class="absolute inset-0 bg-gradient-to-br from-primary-600/90 via-primary-700/80 to-secondary-600/90"></div>
    
    <!-- Floating Elements -->
    {#each floatingElements as element}
      <div 
        class="absolute {element.position} opacity-20 animate-float"
        style="animation-delay: {element.delay}s;"
      >
        <svelte:component this={element.icon} size={40} class="text-white" />
      </div>
    {/each}
    
    <!-- Professional Background Elements -->
    <div class="absolute top-1/4 left-1/4 w-64 h-64 bg-gradient-to-r from-purple-400/10 to-pink-400/10 rounded-full blur-3xl animate-float-slow"></div>
    <div class="absolute bottom-1/4 right-1/4 w-96 h-96 bg-gradient-to-r from-blue-400/8 to-cyan-400/8 rounded-full blur-3xl animate-float" style="animation-delay: 2s;"></div>
    <div class="absolute top-1/2 left-1/2 w-32 h-32 bg-gradient-to-r from-yellow-400/12 to-orange-400/12 rounded-full blur-2xl animate-pulse" style="animation-delay: 1s;"></div>
    
    <!-- Subtle Geometric Accents -->
    <div class="absolute top-20 left-20 w-16 h-16 bg-gradient-to-r from-indigo-500/20 to-purple-500/20 rounded-lg animate-float opacity-60" style="animation-delay: 0.5s;"></div>
    <div class="absolute bottom-20 right-20 w-12 h-12 bg-gradient-to-r from-pink-500/20 to-rose-500/20 rounded-full animate-pulse opacity-60" style="animation-delay: 1.5s;"></div>
    
    <!-- Professional Sparkle Effects -->
    <div class="absolute top-24 right-24 w-2 h-2 bg-white rounded-full animate-sparkle opacity-40" style="animation-delay: 0.5s;"></div>
    <div class="absolute bottom-36 left-36 w-1.5 h-1.5 bg-yellow-300 rounded-full animate-sparkle opacity-40" style="animation-delay: 1.5s;"></div>
    <div class="absolute top-1/3 right-1/4 w-1 h-1 bg-cyan-300 rounded-full animate-sparkle opacity-40" style="animation-delay: 2.5s;"></div>
  </div>

  <div class="relative z-10 container-custom">
    <div class="grid lg:grid-cols-2 gap-12 items-center">
      <!-- Hero Content -->
      <div class="text-white space-y-8 {isVisible ? 'animate-fade-in-left' : 'opacity-0'}">
        <!-- Badge -->
        <div class="inline-flex items-center space-x-2 bg-white/10 backdrop-blur-sm rounded-full px-4 py-2 border border-white/20">
          <div class="w-2 h-2 bg-green-400 rounded-full animate-pulse"></div>
          <span class="text-sm font-medium">Trusted by 500+ Companies</span>
        </div>

        <!-- Main Headline -->
        <div class="space-y-4">
          <h1 class="text-responsive-xl font-bold font-heading leading-tight">
            Transform Your Business with
            <span class="block bg-gradient-to-r from-yellow-300 to-orange-300 bg-clip-text text-transparent">
              Cutting-Edge IT Solutions
            </span>
          </h1>
          
          <p class="text-xl lg:text-2xl text-blue-100 max-w-2xl leading-relaxed">
            We deliver innovative technology solutions that drive growth, enhance efficiency, and secure your digital future.
          </p>
        </div>

        <!-- Stats -->
        <div class="grid grid-cols-3 gap-6 py-6">
          <div class="text-center professional-card p-4 rounded-xl bg-white/5 backdrop-blur-sm border border-white/10">
            <div class="text-3xl font-bold text-yellow-300">500+</div>
            <div class="text-sm text-blue-200">Projects Completed</div>
          </div>
          <div class="text-center professional-card p-4 rounded-xl bg-white/5 backdrop-blur-sm border border-white/10">
            <div class="text-3xl font-bold text-yellow-300">99%</div>
            <div class="text-sm text-blue-200">Client Satisfaction</div>
          </div>
          <div class="text-center professional-card p-4 rounded-xl bg-white/5 backdrop-blur-sm border border-white/10">
            <div class="text-3xl font-bold text-yellow-300">24/7</div>
            <div class="text-sm text-blue-200">Support Available</div>
          </div>
        </div>

        <!-- CTA Buttons -->
        <div class="flex flex-col sm:flex-row gap-4">
          <button
            on:click={scrollToContact}
            class="group bg-white text-primary-700 hover:bg-yellow-300 hover:text-primary-800 font-semibold py-4 px-8 rounded-lg shadow-xl hover:shadow-2xl transform hover:-translate-y-1 transition-all duration-300 ease-out flex items-center justify-center space-x-2 professional-card"
          >
            <span>Get Started Today</span>
            <ArrowRight size={20} class="group-hover:translate-x-1 transition-transform duration-300" />
          </button>
          
          <button
            on:click={scrollToServices}
            class="group bg-transparent border-2 border-white text-white hover:bg-white hover:text-primary-700 font-semibold py-4 px-8 rounded-lg shadow-xl hover:shadow-2xl transform hover:-translate-y-1 transition-all duration-300 ease-out flex items-center justify-center space-x-2 professional-card"
          >
            <Play size={20} class="group-hover:scale-110 transition-transform duration-300" />
            <span>Watch Demo</span>
          </button>
        </div>

        <!-- Trust Indicators -->
        <div class="flex items-center space-x-6 pt-8">
          <span class="text-blue-200 text-sm">Trusted by:</span>
          <div class="flex items-center space-x-4 opacity-70">
            <div class="w-20 h-8 bg-white/20 rounded-md flex items-center justify-center">
              <span class="text-xs font-semibold">TECH</span>
            </div>
            <div class="w-20 h-8 bg-white/20 rounded-md flex items-center justify-center">
              <span class="text-xs font-semibold">CORP</span>
            </div>
            <div class="w-20 h-8 bg-white/20 rounded-md flex items-center justify-center">
              <span class="text-xs font-semibold">START</span>
            </div>
          </div>
        </div>
      </div>

      <!-- Hero Image/Animation -->
      <div class="relative {isVisible ? 'animate-fade-in-right' : 'opacity-0'}" style="animation-delay: 0.3s;">
        <div class="relative">
          <!-- Main Image Container -->
          <div class="relative z-10 bg-white/10 backdrop-blur-sm rounded-2xl p-8 border border-white/20 shadow-2xl">
            <div class="aspect-square bg-gradient-to-br from-white/20 to-white/5 rounded-xl flex items-center justify-center">
              <!-- Dashboard Preview -->
              <div class="w-full h-full bg-white/90 rounded-lg p-6 space-y-4">
                <div class="flex items-center justify-between">
                  <div class="w-20 h-4 bg-primary-200 rounded animate-pulse"></div>
                  <div class="w-4 h-4 bg-green-400 rounded-full animate-pulse"></div>
                </div>
                
                <div class="space-y-3">
                  <div class="h-3 bg-gray-200 rounded animate-pulse"></div>
                  <div class="h-3 bg-gray-200 rounded w-3/4 animate-pulse"></div>
                  <div class="h-3 bg-gray-200 rounded w-1/2 animate-pulse"></div>
                </div>
                
                <div class="grid grid-cols-2 gap-4 pt-4">
                  <div class="h-16 bg-primary-100 rounded-lg animate-pulse"></div>
                  <div class="h-16 bg-secondary-100 rounded-lg animate-pulse"></div>
                </div>
              </div>
            </div>
          </div>

          <!-- Floating Cards -->
          <div class="absolute -top-4 -right-4 w-24 h-16 bg-yellow-400 rounded-lg shadow-lg animate-float" style="animation-delay: 1s;">
            <div class="p-3">
              <div class="text-xs font-semibold text-yellow-900">+25%</div>
              <div class="text-xs text-yellow-800">Growth</div>
            </div>
          </div>
          
          <div class="absolute -bottom-4 -left-4 w-24 h-16 bg-green-400 rounded-lg shadow-lg animate-float" style="animation-delay: 2s;">
            <div class="p-3">
              <div class="text-xs font-semibold text-green-900">99.9%</div>
              <div class="text-xs text-green-800">Uptime</div>
            </div>
          </div>

          <!-- Glow Effect -->
          <div class="absolute inset-0 bg-gradient-to-r from-primary-400 to-secondary-400 rounded-2xl blur-2xl opacity-30 animate-pulse-glow"></div>
        </div>
      </div>
    </div>
  </div>

  <!-- Scroll Indicator -->
  <div class="absolute bottom-8 left-1/2 transform -translate-x-1/2">
    <div class="w-6 h-10 border-2 border-white/50 rounded-full flex justify-center">
      <div class="w-1 h-3 bg-white rounded-full mt-2 animate-bounce"></div>
    </div>
  </div>
</section>
