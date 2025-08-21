<script lang="ts">
  import { onMount } from 'svelte';
  import { Users, Award, Clock, TrendingUp } from 'lucide-svelte';

  let statsRef: HTMLElement;
  let isVisible = false;
  let counters = [
    { current: 0, target: 500, label: 'Projects Completed', icon: Award },
    { current: 0, target: 250, label: 'Happy Clients', icon: Users },
    { current: 0, target: 99, label: 'Success Rate (%)', icon: TrendingUp },
    { current: 0, target: 5, label: 'Years Experience', icon: Clock }
  ];

  const animateCounter = (index: number) => {
    const target = counters[index].target;
    const increment = target / 100;
    const timer = setInterval(() => {
      if (counters[index].current < target) {
        counters[index].current = Math.min(counters[index].current + increment, target);
        counters = [...counters]; // Trigger reactivity
      } else {
        clearInterval(timer);
      }
    }, 20);
  };

  onMount(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting && !isVisible) {
            isVisible = true;
            // Start counter animations with delays
            counters.forEach((_, index) => {
              setTimeout(() => animateCounter(index), index * 200);
            });
          }
        });
      },
      { threshold: 0.5 }
    );

    if (statsRef) {
      observer.observe(statsRef);
    }

    return () => observer.disconnect();
  });
</script>

<section 
  bind:this={statsRef}
  class="py-16 bg-gradient-to-r from-primary-600 to-primary-700 relative overflow-hidden"
>
  <!-- Background Pattern -->
  <div class="absolute inset-0 opacity-10">
    <div class="absolute inset-0 bg-[url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNjAiIGhlaWdodD0iNjAiIHZpZXdCb3g9IjAgMCA2MCA2MCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZyBmaWxsPSJub25lIiBmaWxsLXJ1bGU9ImV2ZW5vZGQiPjxnIGZpbGw9IiNmZmZmZmYiIGZpbGwtb3BhY2l0eT0iMC4xIj48Y2lyY2xlIGN4PSIzMCIgY3k9IjMwIiByPSI0Ii8+PC9nPjwvZz48L3N2Zz4=')]"></div>
  </div>

  <div class="container-custom relative z-10">
    <div class="grid grid-cols-2 lg:grid-cols-4 gap-8">
      {#each counters as stat, index}
        <div 
          class="text-center group {isVisible ? 'animate-fade-in-up' : 'opacity-0'}"
          style="animation-delay: {index * 0.1}s;"
        >
          <div class="flex flex-col items-center space-y-4">
            <!-- Icon -->
            <div class="w-16 h-16 bg-white/10 rounded-full flex items-center justify-center group-hover:bg-white/20 transition-colors duration-300 group-hover:scale-110 transform transition-transform">
              <svelte:component this={stat.icon} size={28} class="text-white" />
            </div>
            
            <!-- Counter -->
            <div class="space-y-2">
              <div class="text-3xl lg:text-4xl font-bold text-white">
                {Math.round(stat.current)}{stat.label.includes('%') ? '%' : '+'}
              </div>
              <div class="text-blue-100 font-medium text-sm lg:text-base">
                {stat.label}
              </div>
            </div>
          </div>
        </div>
      {/each}
    </div>

    <!-- Bottom Text -->
    <div class="text-center mt-12 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.5s;">
      <p class="text-blue-100 text-lg max-w-2xl mx-auto">
        Trusted by businesses worldwide to deliver exceptional IT solutions that drive real results.
      </p>
    </div>
  </div>
</section>
