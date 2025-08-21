<script lang="ts">
  import { onMount } from 'svelte';
  import { Mail, Phone, MapPin, Clock, Send, CheckCircle } from 'lucide-svelte';

  let contactRef: HTMLElement;
  let isVisible = false;
  let isSubmitting = false;
  let showSuccess = false;
  
  let formData = {
    name: '',
    email: '',
    company: '',
    phone: '',
    service: '',
    message: ''
  };

  const services = [
    'Web Development',
    'Cloud Solutions',
    'Cybersecurity',
    'Mobile Apps',
    'Data Analytics',
    'IT Support',
    'Other'
  ];

  const contactInfo = [
    {
      icon: Mail,
      label: 'Email',
      value: 'info@brandit.com',
      link: 'mailto:info@brandit.com'
    },
    {
      icon: Phone,
      label: 'Phone',
      value: '+1 (555) 123-4567',
      link: 'tel:+15551234567'
    },
    {
      icon: MapPin,
      label: 'Address',
      value: '123 Business Ave, Tech City, TC 12345',
      link: 'https://maps.google.com'
    },
    {
      icon: Clock,
      label: 'Business Hours',
      value: 'Mon-Fri: 9AM-6PM EST',
      link: null
    }
  ];

  const handleSubmit = async (e: Event) => {
    e.preventDefault();
    isSubmitting = true;

    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 2000));
    
    isSubmitting = false;
    showSuccess = true;
    
    // Reset form
    formData = {
      name: '',
      email: '',
      company: '',
      phone: '',
      service: '',
      message: ''
    };

    setTimeout(() => {
      showSuccess = false;
    }, 5000);
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

    if (contactRef) {
      observer.observe(contactRef);
    }

    return () => observer.disconnect();
  });
</script>

<section bind:this={contactRef} class="section-padding bg-white">
  <div class="container-custom">
    <!-- Section Header -->
    <div class="text-center mb-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}">
      <div class="inline-flex items-center space-x-2 bg-primary-100 text-primary-700 px-4 py-2 rounded-full text-sm font-semibold mb-4">
        <span class="w-2 h-2 bg-primary-500 rounded-full"></span>
        <span>Get in Touch</span>
      </div>
      
      <h2 class="text-responsive-lg font-bold text-gray-900 mb-6">
        Ready to Start Your
        <span class="text-gradient">Digital Journey?</span>
      </h2>
      
      <p class="text-xl text-gray-600 max-w-3xl mx-auto">
        Let's discuss how we can help transform your business with our innovative IT solutions.
        Get in touch with our experts today.
      </p>
    </div>

    <div class="grid lg:grid-cols-3 gap-12">
      <!-- Contact Information -->
      <div class="lg:col-span-1 space-y-8 {isVisible ? 'animate-fade-in-left' : 'opacity-0'}" style="animation-delay: 0.2s;">
        <div>
          <h3 class="text-2xl font-bold text-gray-900 mb-6">Contact Information</h3>
          <p class="text-gray-600 mb-8">
            We're here to help! Reach out to us through any of the following channels.
          </p>
        </div>

        <div class="space-y-6">
          {#each contactInfo as info}
            <div class="flex items-start space-x-4 group professional-card p-4 rounded-xl">
              <div class="w-12 h-12 bg-primary-100 rounded-lg flex items-center justify-center group-hover:bg-primary-200 transition-colors duration-300">
                <svelte:component this={info.icon} size={20} class="text-primary-600" />
              </div>
              <div>
                <h4 class="font-semibold text-gray-900">{info.label}</h4>
                {#if info.link}
                  <a href={info.link} class="text-gray-600 hover:text-primary-600 transition-colors duration-300">
                    {info.value}
                  </a>
                {:else}
                  <p class="text-gray-600">{info.value}</p>
                {/if}
              </div>
            </div>
          {/each}
        </div>

        <!-- Quick Contact -->
        <div class="bg-gradient-to-br from-primary-500 to-secondary-500 rounded-2xl p-6 text-white">
          <h4 class="text-xl font-bold mb-3">Need Immediate Help?</h4>
          <p class="text-blue-100 mb-4">
            For urgent technical support or emergency assistance.
          </p>
          <a 
            href="tel:+15551234567" 
            class="inline-flex items-center space-x-2 bg-white/20 hover:bg-white/30 px-4 py-2 rounded-lg transition-colors duration-300"
          >
            <Phone size={16} />
            <span>Call Now</span>
          </a>
        </div>
      </div>

      <!-- Contact Form -->
      <div class="lg:col-span-2 {isVisible ? 'animate-fade-in-right' : 'opacity-0'}" style="animation-delay: 0.4s;">
        <div class="bg-gray-50 rounded-2xl p-8 shadow-lg">
          <h3 class="text-2xl font-bold text-gray-900 mb-6">Send us a Message</h3>
          
          {#if showSuccess}
            <div class="bg-green-50 border border-green-200 rounded-lg p-4 mb-6 flex items-center space-x-3">
              <CheckCircle size={20} class="text-green-600" />
              <div>
                <h4 class="font-semibold text-green-800">Message Sent Successfully!</h4>
                <p class="text-green-600 text-sm">We'll get back to you within 24 hours.</p>
              </div>
            </div>
          {/if}

          <form on:submit={handleSubmit} class="space-y-6">
            <!-- Name and Email -->
            <div class="grid md:grid-cols-2 gap-6">
              <div>
                <label for="name" class="block text-sm font-semibold text-gray-700 mb-2">
                  Full Name *
                </label>
                <input
                  type="text"
                  id="name"
                  bind:value={formData.name}
                  required
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300"
                  placeholder="Your full name"
                />
              </div>
              
              <div>
                <label for="email" class="block text-sm font-semibold text-gray-700 mb-2">
                  Email Address *
                </label>
                <input
                  type="email"
                  id="email"
                  bind:value={formData.email}
                  required
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300"
                  placeholder="your.email@company.com"
                />
              </div>
            </div>

            <!-- Company and Phone -->
            <div class="grid md:grid-cols-2 gap-6">
              <div>
                <label for="company" class="block text-sm font-semibold text-gray-700 mb-2">
                  Company
                </label>
                <input
                  type="text"
                  id="company"
                  bind:value={formData.company}
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300"
                  placeholder="Your company name"
                />
              </div>
              
              <div>
                <label for="phone" class="block text-sm font-semibold text-gray-700 mb-2">
                  Phone Number
                </label>
                <input
                  type="tel"
                  id="phone"
                  bind:value={formData.phone}
                  class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300"
                  placeholder="+1 (555) 123-4567"
                />
              </div>
            </div>

            <!-- Service Interest -->
            <div>
              <label for="service" class="block text-sm font-semibold text-gray-700 mb-2">
                Service of Interest
              </label>
              <select
                id="service"
                bind:value={formData.service}
                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300"
              >
                <option value="">Select a service</option>
                {#each services as service}
                  <option value={service}>{service}</option>
                {/each}
              </select>
            </div>

            <!-- Message -->
            <div>
              <label for="message" class="block text-sm font-semibold text-gray-700 mb-2">
                Message *
              </label>
              <textarea
                id="message"
                bind:value={formData.message}
                required
                rows="5"
                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300 resize-none"
                placeholder="Tell us about your project or how we can help you..."
              ></textarea>
            </div>

            <!-- Submit Button -->
            <div>
              <button
                type="submit"
                disabled={isSubmitting}
                class="w-full bg-gradient-to-r from-primary-600 to-primary-700 hover:from-primary-700 hover:to-primary-800 disabled:from-gray-400 disabled:to-gray-500 text-white font-semibold py-4 px-8 rounded-lg shadow-lg hover:shadow-xl transform hover:-translate-y-1 disabled:hover:translate-y-0 transition-all duration-300 ease-out flex items-center justify-center space-x-2 professional-card"
              >
                {#if isSubmitting}
                  <div class="spinner"></div>
                  <span>Sending Message...</span>
                {:else}
                  <Send size={20} />
                  <span>Send Message</span>
                {/if}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- FAQ Section -->
    <div class="mt-24 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.6s;">
      <div class="text-center mb-12">
        <h3 class="text-responsive-md font-bold text-gray-900 mb-4">
          Frequently Asked Questions
        </h3>
      </div>

      <div class="grid md:grid-cols-2 gap-8 max-w-4xl mx-auto">
        <div class="space-y-6">
          <div>
            <h4 class="font-semibold text-gray-900 mb-2">How quickly do you respond?</h4>
            <p class="text-gray-600 text-sm">We typically respond to all inquiries within 24 hours during business days.</p>
          </div>
          
          <div>
            <h4 class="font-semibold text-gray-900 mb-2">Do you offer free consultations?</h4>
            <p class="text-gray-600 text-sm">Yes! We provide free initial consultations to understand your needs and propose solutions.</p>
          </div>
        </div>
        
        <div class="space-y-6">
          <div>
            <h4 class="font-semibold text-gray-900 mb-2">What's your project timeline?</h4>
            <p class="text-gray-600 text-sm">Project timelines vary based on scope, but we provide detailed estimates during consultation.</p>
          </div>
          
          <div>
            <h4 class="font-semibold text-gray-900 mb-2">Do you provide ongoing support?</h4>
            <p class="text-gray-600 text-sm">Absolutely! We offer 24/7 support and maintenance packages for all our solutions.</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
