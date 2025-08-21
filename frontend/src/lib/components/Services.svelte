<script lang="ts">
  import { onMount } from 'svelte';
  import { 
    Code, 
    Cloud, 
    Shield, 
    Smartphone, 
    BarChart3, 
    Headphones,
    ArrowRight,
    CheckCircle
  } from 'lucide-svelte';

  let servicesRef: HTMLElement;
  let isVisible = false;

  // Mouse tracking for 3D effects
  const handleMouseMove = (e: MouseEvent, element: HTMLElement) => {
    const rect = element.getBoundingClientRect();
    const centerX = rect.left + rect.width / 2;
    const centerY = rect.top + rect.height / 2;
    
    const mouseX = e.clientX - centerX;
    const mouseY = e.clientY - centerY;
    
    const rotateX = (mouseY / rect.height) * -10;
    const rotateY = (mouseX / rect.width) * 10;
    
    element.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) translateZ(10px)`;
  };

  const handleMouseLeave = (element: HTMLElement) => {
    element.style.transform = 'perspective(1000px) rotateX(0deg) rotateY(0deg) translateZ(0px)';
  };

  const services = [
    {
      icon: Code,
      title: 'Web Development',
      description: 'Custom web applications built with modern technologies for optimal performance and user experience.',
      features: ['Responsive Design', 'Modern Frameworks', 'SEO Optimized', 'Fast Loading'],
      color: 'from-blue-500 to-blue-600',
      bgColor: 'bg-blue-50',
      textColor: 'text-blue-600'
    },
    {
      icon: Cloud,
      title: 'Cloud Solutions',
      description: 'Scalable cloud infrastructure and migration services to optimize your business operations.',
      features: ['AWS/Azure/GCP', 'Auto Scaling', '99.9% Uptime', 'Cost Optimization'],
      color: 'from-purple-500 to-purple-600',
      bgColor: 'bg-purple-50',
      textColor: 'text-purple-600'
    },
    {
      icon: Shield,
      title: 'Cybersecurity',
      description: 'Comprehensive security solutions to protect your digital assets and ensure compliance.',
      features: ['Threat Protection', 'Compliance', 'Security Audits', '24/7 Monitoring'],
      color: 'from-green-500 to-green-600',
      bgColor: 'bg-green-50',
      textColor: 'text-green-600'
    },
    {
      icon: Smartphone,
      title: 'Mobile Apps',
      description: 'Native and cross-platform mobile applications that engage users and drive business growth.',
      features: ['iOS & Android', 'Cross-Platform', 'UI/UX Design', 'App Store Support'],
      color: 'from-orange-500 to-orange-600',
      bgColor: 'bg-orange-50',
      textColor: 'text-orange-600'
    },
    {
      icon: BarChart3,
      title: 'Data Analytics',
      description: 'Transform your data into actionable insights with advanced analytics and visualization.',
      features: ['Business Intelligence', 'Real-time Analytics', 'Custom Dashboards', 'Predictive Analysis'],
      color: 'from-pink-500 to-pink-600',
      bgColor: 'bg-pink-50',
      textColor: 'text-pink-600'
    },
    {
      icon: Headphones,
      title: 'IT Support',
      description: '24/7 technical support and maintenance to keep your systems running smoothly.',
      features: ['24/7 Support', 'Remote Assistance', 'System Maintenance', 'Quick Response'],
      color: 'from-teal-500 to-teal-600',
      bgColor: 'bg-teal-50',
      textColor: 'text-teal-600'
    }
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

    if (servicesRef) {
      observer.observe(servicesRef);
    }

    return () => observer.disconnect();
  });
</script>

<section bind:this={servicesRef} class="section-padding bg-gray-50 dark:bg-gray-800 transition-colors duration-300">
  <div class="container-custom">
    <!-- Section Header -->
    <div class="text-center mb-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}">
      <div class="inline-flex items-center space-x-2 bg-primary-100 dark:bg-primary-900 text-primary-700 dark:text-primary-300 px-4 py-2 rounded-full text-sm font-semibold mb-4">
        <span class="w-2 h-2 bg-primary-500 rounded-full"></span>
        <span>Our Services</span>
      </div>
      
      <h2 class="text-responsive-lg font-bold text-gray-900 dark:text-white mb-6">
        Comprehensive IT Solutions for
        <span class="text-gradient">Your Success</span>
      </h2>
      
      <p class="text-xl text-gray-600 dark:text-gray-300 max-w-3xl mx-auto">
        We provide end-to-end technology services that help businesses thrive in the digital age.
        From web development to cloud solutions, we've got you covered.
      </p>
    </div>

    <!-- Enhanced Services Grid -->
    <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
      {#each services as service, index}
        <div 
          class="group relative bg-white dark:bg-gray-800 rounded-2xl p-8 shadow-lg border border-gray-100 dark:border-gray-700 stagger-item card-3d {isVisible ? 'animate-fadeInScale' : 'opacity-0'} transition-colors duration-300"
          style="animation-delay: {index * 0.15}s;"
          on:mousemove={(e) => handleMouseMove(e, e.currentTarget)}
          on:mouseleave={(e) => handleMouseLeave(e.currentTarget)}
        >
          <!-- Professional Icon -->
          <div class="relative mb-6">
            <div class="w-16 h-16 bg-gradient-to-r {service.color} rounded-xl flex items-center justify-center shadow-lg group-hover:scale-105 transform transition-all duration-300">
              <svelte:component this={service.icon} size={28} class="text-white" />
            </div>
            
            <!-- Professional Badge -->
            <div class="absolute -top-2 -right-2 w-6 h-6 bg-green-400 rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-all duration-300">
              <CheckCircle size={16} class="text-white" />
            </div>
            
            <!-- Subtle Glow Effect -->
            <div class="absolute inset-0 w-16 h-16 bg-gradient-to-r {service.color} rounded-xl opacity-0 group-hover:opacity-20 blur-lg transition-opacity duration-300"></div>
          </div>

          <!-- Professional Content -->
          <div class="space-y-4">
            <h3 class="text-xl font-bold text-gray-900 dark:text-white group-hover:{service.textColor} transition-colors duration-300">
              {service.title}
            </h3>
            
            <p class="text-gray-600 dark:text-gray-300 leading-relaxed">
              {service.description}
            </p>

            <!-- Features -->
            <ul class="space-y-2">
              {#each service.features as feature}
                <li class="flex items-center space-x-2 text-sm text-gray-600 dark:text-gray-400">
                  <div class="w-1.5 h-1.5 bg-gradient-to-r {service.color} rounded-full"></div>
                  <span>{feature}</span>
                </li>
              {/each}
            </ul>
          </div>

          <!-- Professional CTA -->
          <div class="mt-6 pt-6 border-t border-gray-100 dark:border-gray-700">
            <button class="group/btn flex items-center space-x-2 {service.textColor} dark:text-primary-400 hover:text-gray-900 dark:hover:text-white font-semibold transition-colors duration-300 w-full justify-center py-3 px-6 rounded-lg bg-gradient-to-r from-gray-50 to-white dark:from-gray-700 dark:to-gray-600 shadow-lg hover:shadow-xl professional-card">
              <span>Learn More</span>
              <ArrowRight size={16} class="group-hover/btn:translate-x-1 transition-transform duration-300" />
            </button>
          </div>

          <!-- Hover Effect -->
          <div class="absolute inset-0 bg-gradient-to-r {service.color} opacity-0 group-hover:opacity-5 rounded-2xl transition-opacity duration-300"></div>
        </div>
      {/each}
    </div>

    <!-- Bottom CTA -->
    <div class="text-center mt-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.8s;">
      <div class="bg-white rounded-2xl p-8 shadow-lg border border-gray-100 max-w-2xl mx-auto">
        <h3 class="text-2xl font-bold text-gray-900 mb-4">
          Ready to Transform Your Business?
        </h3>
        <p class="text-gray-600 mb-6">
          Let's discuss how our IT solutions can help you achieve your goals.
        </p>
        <button class="btn-primary">
          Get Free Consultation
        </button>
      </div>
    </div>
  </div>
</section>
