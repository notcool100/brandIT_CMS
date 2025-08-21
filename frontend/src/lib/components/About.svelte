<script lang="ts">
  import { onMount } from 'svelte';
  import { Target, Users, Award, TrendingUp, CheckCircle, Play } from 'lucide-svelte';

  let aboutRef: HTMLElement;
  let isVisible = false;

  const values = [
    {
      icon: Target,
      title: 'Mission Driven',
      description: 'We are committed to delivering innovative solutions that drive real business value.',
      color: 'text-blue-600'
    },
    {
      icon: Users,
      title: 'Client Focused',
      description: 'Your success is our priority. We build lasting partnerships through exceptional service.',
      color: 'text-green-600'
    },
    {
      icon: Award,
      title: 'Quality Excellence',
      description: 'We maintain the highest standards in everything we do, ensuring superior outcomes.',
      color: 'text-purple-600'
    },
    {
      icon: TrendingUp,
      title: 'Continuous Growth',
      description: 'We stay ahead of technology trends to provide cutting-edge solutions.',
      color: 'text-orange-600'
    }
  ];

  const achievements = [
    { metric: '500+', label: 'Successful Projects' },
    { metric: '250+', label: 'Happy Clients' },
    { metric: '5+', label: 'Years of Excellence' },
    { metric: '24/7', label: 'Support Available' }
  ];

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

    if (aboutRef) {
      observer.observe(aboutRef);
    }

    return () => observer.disconnect();
  });
</script>

<section bind:this={aboutRef} class="section-padding bg-white">
  <div class="container-custom">
    <div class="grid lg:grid-cols-2 gap-16 items-center">
      <!-- Content -->
      <div class="space-y-8 {isVisible ? 'animate-fade-in-left' : 'opacity-0'}">
        <!-- Section Header -->
        <div>
          <div class="inline-flex items-center space-x-2 bg-primary-100 text-primary-700 px-4 py-2 rounded-full text-sm font-semibold mb-4">
            <span class="w-2 h-2 bg-primary-500 rounded-full"></span>
            <span>About Us</span>
          </div>
          
          <h2 class="text-responsive-lg font-bold text-gray-900 mb-6">
            Empowering Businesses Through
            <span class="text-gradient">Technology Innovation</span>
          </h2>
          
          <p class="text-xl text-gray-600 leading-relaxed">
            Brand IT Solution is a leading technology company dedicated to transforming businesses 
            through innovative IT solutions. We combine technical expertise with business acumen 
            to deliver results that matter.
          </p>
        </div>

        <!-- Story -->
        <div class="space-y-4">
          <h3 class="text-2xl font-bold text-gray-900">Our Story</h3>
          <p class="text-gray-600 leading-relaxed">
            Founded with a vision to bridge the gap between technology and business success, 
            we have grown from a small startup to a trusted partner for companies worldwide. 
            Our journey is marked by continuous innovation, unwavering commitment to quality, 
            and a passion for solving complex challenges.
          </p>
        </div>

        <!-- Key Points -->
        <div class="space-y-4">
          <div class="flex items-start space-x-3">
            <CheckCircle size={20} class="text-green-500 mt-1 flex-shrink-0" />
            <div>
              <h4 class="font-semibold text-gray-900">Expert Team</h4>
              <p class="text-gray-600">Certified professionals with deep industry expertise</p>
            </div>
          </div>
          
          <div class="flex items-start space-x-3">
            <CheckCircle size={20} class="text-green-500 mt-1 flex-shrink-0" />
            <div>
              <h4 class="font-semibold text-gray-900">Proven Track Record</h4>
              <p class="text-gray-600">500+ successful projects across various industries</p>
            </div>
          </div>
          
          <div class="flex items-start space-x-3">
            <CheckCircle size={20} class="text-green-500 mt-1 flex-shrink-0" />
            <div>
              <h4 class="font-semibold text-gray-900">24/7 Support</h4>
              <p class="text-gray-600">Round-the-clock assistance to keep your business running</p>
            </div>
          </div>
        </div>

        <!-- CTA -->
        <div class="flex flex-col sm:flex-row gap-4">
          <button class="btn-primary professional-card">
            Learn More About Us
          </button>
          <button class="btn-secondary flex items-center space-x-2 professional-card">
            <Play size={20} />
            <span>Watch Our Story</span>
          </button>
        </div>
      </div>

      <!-- Visual Content -->
      <div class="relative {isVisible ? 'animate-fade-in-right' : 'opacity-0'}" style="animation-delay: 0.3s;">
        <!-- Main Image Container -->
        <div class="relative">
          <div class="bg-gradient-to-br from-primary-500 to-secondary-500 rounded-2xl p-8 shadow-2xl professional-card">
            <div class="bg-white rounded-xl p-6 space-y-6">
              <!-- Team Photo Placeholder -->
              <div class="aspect-video bg-gradient-to-br from-gray-100 to-gray-200 rounded-lg flex items-center justify-center">
                <div class="text-center space-y-2">
                  <Users size={48} class="text-gray-400 mx-auto" />
                  <p class="text-gray-500 font-medium">Our Expert Team</p>
                </div>
              </div>
              
              <!-- Achievement Grid -->
              <div class="grid grid-cols-2 gap-4">
                {#each achievements as achievement}
                  <div class="text-center p-3 bg-gray-50 rounded-lg">
                    <div class="text-2xl font-bold text-primary-600">{achievement.metric}</div>
                    <div class="text-sm text-gray-600">{achievement.label}</div>
                  </div>
                {/each}
              </div>
            </div>
          </div>

          <!-- Floating Elements -->
          <div class="absolute -top-4 -right-4 w-20 h-20 bg-yellow-400 rounded-full flex items-center justify-center shadow-lg animate-float">
            <Award size={32} class="text-yellow-900" />
          </div>
          
          <div class="absolute -bottom-4 -left-4 w-16 h-16 bg-green-400 rounded-full flex items-center justify-center shadow-lg animate-float" style="animation-delay: 2s;">
            <TrendingUp size={24} class="text-green-900" />
          </div>
        </div>
      </div>
    </div>

    <!-- Values Section -->
    <div class="mt-24">
      <div class="text-center mb-12 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.6s;">
        <h3 class="text-responsive-md font-bold text-gray-900 mb-4">
          Our Core Values
        </h3>
        <p class="text-xl text-gray-600 max-w-2xl mx-auto">
          The principles that guide everything we do and shape our company culture.
        </p>
      </div>

      <div class="grid md:grid-cols-2 lg:grid-cols-4 gap-8">
        {#each values as value, index}
          <div 
            class="text-center group {isVisible ? 'animate-fade-in-up' : 'opacity-0'}"
            style="animation-delay: {0.8 + index * 0.1}s;"
          >
            <div class="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4 group-hover:bg-primary-100 transition-colors duration-300">
              <svelte:component this={value.icon} size={28} class="{value.color} group-hover:scale-110 transform transition-transform duration-300" />
            </div>
            
            <h4 class="text-xl font-bold text-gray-900 mb-2">{value.title}</h4>
            <p class="text-gray-600 text-sm leading-relaxed">{value.description}</p>
          </div>
        {/each}
      </div>
    </div>
  </div>
</section>
