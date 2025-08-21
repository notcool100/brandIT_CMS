<script lang="ts">
  import { onMount } from 'svelte';
  import { Star, Quote } from 'lucide-svelte';

  let testimonialsRef: HTMLElement;
  let isVisible = false;
  let currentTestimonial = 0;

  const testimonials = [
    {
      name: 'Jennifer Martinez',
      role: 'CEO',
      company: 'TechStart Inc.',
      image: '/api/placeholder/80/80',
      rating: 5,
      content: 'Brand IT Solution transformed our entire digital infrastructure. Their expertise in cloud migration saved us 40% in operational costs while improving performance dramatically. Absolutely outstanding service!'
    },
    {
      name: 'Robert Chen',
      role: 'CTO',
      company: 'DataFlow Systems',
      image: '/api/placeholder/80/80',
      rating: 5,
      content: 'Working with Brand IT has been a game-changer. Their cybersecurity solutions protected us from multiple threats, and their 24/7 support gives us peace of mind. Highly recommended for any business.'
    },
    {
      name: 'Sarah Thompson',
      role: 'Founder',
      company: 'Digital Ventures',
      image: '/api/placeholder/80/80',
      rating: 5,
      content: 'The web application they built for us exceeded all expectations. Modern, fast, and user-friendly. Our customer engagement increased by 60% since launch. Truly professional team!'
    },
    {
      name: 'Michael Rodriguez',
      role: 'Operations Director',
      company: 'GlobalTech Corp',
      image: '/api/placeholder/80/80',
      rating: 5,
      content: 'Brand IT Solution delivered our mobile app on time and under budget. The quality is exceptional, and their post-launch support has been phenomenal. They understand business needs perfectly.'
    },
    {
      name: 'Emily Johnson',
      role: 'VP Technology',
      company: 'InnovateNow',
      image: '/api/placeholder/80/80',
      rating: 5,
      content: 'Their data analytics platform provided insights we never knew existed in our business. Revenue increased by 25% in just 6 months. The ROI has been incredible. Thank you, Brand IT!'
    }
  ];

  const nextTestimonial = () => {
    currentTestimonial = (currentTestimonial + 1) % testimonials.length;
  };

  const prevTestimonial = () => {
    currentTestimonial = currentTestimonial === 0 ? testimonials.length - 1 : currentTestimonial - 1;
  };

  const goToTestimonial = (index: number) => {
    currentTestimonial = index;
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

    if (testimonialsRef) {
      observer.observe(testimonialsRef);
    }

    // Auto-rotate testimonials
    const interval = setInterval(nextTestimonial, 5000);

    return () => {
      observer.disconnect();
      clearInterval(interval);
    };
  });
</script>

<section bind:this={testimonialsRef} class="section-padding bg-gradient-to-br from-primary-50 to-secondary-50">
  <div class="container-custom">
    <!-- Section Header -->
    <div class="text-center mb-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}">
      <div class="inline-flex items-center space-x-2 bg-primary-100 text-primary-700 px-4 py-2 rounded-full text-sm font-semibold mb-4">
        <span class="w-2 h-2 bg-primary-500 rounded-full"></span>
        <span>Testimonials</span>
      </div>
      
      <h2 class="text-responsive-lg font-bold text-gray-900 mb-6">
        What Our Clients Say About
        <span class="text-gradient">Our Services</span>
      </h2>
      
      <p class="text-xl text-gray-600 max-w-3xl mx-auto">
        Don't just take our word for it. Hear from the businesses we've helped transform 
        with our innovative IT solutions.
      </p>
    </div>

    <!-- Testimonial Carousel -->
    <div class="relative max-w-4xl mx-auto {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.3s;">
      <div class="bg-white rounded-2xl shadow-2xl p-8 md:p-12 relative overflow-hidden">
        <!-- Quote Icon -->
        <div class="absolute top-8 right-8 opacity-10">
          <Quote size={80} class="text-primary-600" />
        </div>

        <!-- Testimonial Content -->
        <div class="relative z-10">
          <div class="text-center space-y-6">
            <!-- Stars -->
            <div class="flex justify-center space-x-1">
              {#each Array(testimonials[currentTestimonial].rating) as _}
                <Star size={24} class="text-yellow-400 fill-current" />
              {/each}
            </div>

            <!-- Content -->
            <blockquote class="text-xl lg:text-2xl text-gray-700 font-medium leading-relaxed italic">
              "{testimonials[currentTestimonial].content}"
            </blockquote>

            <!-- Author -->
            <div class="flex items-center justify-center space-x-4 pt-6">
              <!-- Avatar -->
              <div class="w-16 h-16 bg-gradient-to-br from-primary-400 to-secondary-400 rounded-full flex items-center justify-center shadow-lg">
                <span class="text-white text-xl font-bold">
                  {testimonials[currentTestimonial].name.split(' ').map(n => n[0]).join('')}
                </span>
              </div>
              
              <!-- Author Info -->
              <div class="text-left">
                <div class="font-bold text-gray-900 text-lg">
                  {testimonials[currentTestimonial].name}
                </div>
                <div class="text-primary-600 font-semibold">
                  {testimonials[currentTestimonial].role}
                </div>
                <div class="text-gray-600 text-sm">
                  {testimonials[currentTestimonial].company}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Navigation Dots -->
      <div class="flex justify-center space-x-3 mt-8">
        {#each testimonials as _, index}
          <button
            on:click={() => goToTestimonial(index)}
            class="w-3 h-3 rounded-full transition-all duration-300 {index === currentTestimonial ? 'bg-primary-600 scale-125' : 'bg-gray-300 hover:bg-gray-400'}"
            aria-label="Go to testimonial {index + 1}"
          ></button>
        {/each}
      </div>

      <!-- Navigation Arrows -->
      <button
        on:click={prevTestimonial}
        class="absolute left-4 top-1/2 transform -translate-y-1/2 w-12 h-12 bg-white rounded-full shadow-lg hover:shadow-xl flex items-center justify-center text-gray-600 hover:text-primary-600 transition-all duration-300 hover:scale-110"
        aria-label="Previous testimonial"
      >
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
          <polyline points="15,18 9,12 15,6"></polyline>
        </svg>
      </button>
      
      <button
        on:click={nextTestimonial}
        class="absolute right-4 top-1/2 transform -translate-y-1/2 w-12 h-12 bg-white rounded-full shadow-lg hover:shadow-xl flex items-center justify-center text-gray-600 hover:text-primary-600 transition-all duration-300 hover:scale-110"
        aria-label="Next testimonial"
      >
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
          <polyline points="9,18 15,12 9,6"></polyline>
        </svg>
      </button>
    </div>

    <!-- Stats Grid -->
    <div class="grid grid-cols-2 lg:grid-cols-4 gap-8 mt-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.6s;">
      <div class="text-center">
        <div class="text-3xl font-bold text-primary-600 mb-2">98%</div>
        <div class="text-gray-600 text-sm">Client Satisfaction</div>
      </div>
      <div class="text-center">
        <div class="text-3xl font-bold text-primary-600 mb-2">250+</div>
        <div class="text-gray-600 text-sm">Happy Clients</div>
      </div>
      <div class="text-center">
        <div class="text-3xl font-bold text-primary-600 mb-2">4.9</div>
        <div class="text-gray-600 text-sm">Average Rating</div>
      </div>
      <div class="text-center">
        <div class="text-3xl font-bold text-primary-600 mb-2">500+</div>
        <div class="text-gray-600 text-sm">Projects Delivered</div>
      </div>
    </div>

    <!-- CTA Section -->
    <div class="text-center mt-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.8s;">
      <div class="bg-white rounded-2xl p-8 shadow-lg border border-gray-100 max-w-2xl mx-auto">
        <h3 class="text-2xl font-bold text-gray-900 mb-4">
          Ready to Join Our Success Stories?
        </h3>
        <p class="text-gray-600 mb-6">
          Let us help you achieve the same level of success. Start your transformation today.
        </p>
        <button class="btn-primary">
          Start Your Project
        </button>
      </div>
    </div>
  </div>
</section>
