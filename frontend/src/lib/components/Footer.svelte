<script lang="ts">
  import { Mail, Phone, MapPin, Facebook, Twitter, Linkedin, Github, ArrowUp } from 'lucide-svelte';

  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const currentYear = new Date().getFullYear();

  const quickLinks = [
    { label: 'About Us', href: '#about' },
    { label: 'Services', href: '#services' },
    { label: 'Team', href: '#team' },
    { label: 'Blog', href: '#blog' },
    { label: 'Contact', href: '#contact' }
  ];

  const services = [
    { label: 'Web Development', href: '#services' },
    { label: 'Cloud Solutions', href: '#services' },
    { label: 'Cybersecurity', href: '#services' },
    { label: 'Mobile Apps', href: '#services' },
    { label: 'IT Support', href: '#services' }
  ];

  const socialLinks = [
    { icon: Facebook, href: '#', label: 'Facebook' },
    { icon: Twitter, href: '#', label: 'Twitter' },
    { icon: Linkedin, href: '#', label: 'LinkedIn' },
    { icon: Github, href: '#', label: 'GitHub' }
  ];

  const scrollToSection = (sectionId: string) => {
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  };
</script>

<footer class="bg-gray-900 dark:bg-gray-950 text-white relative overflow-hidden transition-colors duration-300">
  <!-- Background Pattern -->
  <div class="absolute inset-0 opacity-5">
    <div class="absolute inset-0 bg-[url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNjAiIGhlaWdodD0iNjAiIHZpZXdCb3g9IjAgMCA2MCA2MCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZyBmaWxsPSJub25lIiBmaWxsLXJ1bGU9ImV2ZW5vZGQiPjxnIGZpbGw9IiNmZmZmZmYiIGZpbGwtb3BhY2l0eT0iMC4xIj48Y2lyY2xlIGN4PSIzMCIgY3k9IjMwIiByPSI0Ii8+PC9nPjwvZz48L3N2Zz4=')]"></div>
  </div>

  <div class="relative z-10">
    <!-- Main Footer Content -->
    <div class="container-custom py-16">
      <div class="grid lg:grid-cols-4 md:grid-cols-2 gap-8">
        <!-- Company Info -->
        <div class="lg:col-span-1 space-y-6">
          <div class="flex items-center space-x-2">
            <div class="w-10 h-10 bg-gradient-to-br from-primary-500 to-primary-700 rounded-lg flex items-center justify-center">
              <span class="text-white font-bold text-xl">B</span>
            </div>
            <span class="text-xl font-bold font-heading">Brand IT Solution</span>
          </div>
          
          <p class="text-gray-300 leading-relaxed">
            Transforming businesses through innovative technology solutions. 
            We deliver excellence in every project, ensuring your digital success.
          </p>

          <!-- Contact Info -->
          <div class="space-y-3">
            <div class="flex items-center space-x-3 text-gray-300">
              <Mail size={16} class="text-primary-400" />
              <a href="mailto:info@brandit.com" class="hover:text-white transition-colors duration-300">
                info@brandit.com
              </a>
            </div>
            
            <div class="flex items-center space-x-3 text-gray-300">
              <Phone size={16} class="text-primary-400" />
              <a href="tel:+15551234567" class="hover:text-white transition-colors duration-300">
                +1 (555) 123-4567
              </a>
            </div>
            
            <div class="flex items-start space-x-3 text-gray-300">
              <MapPin size={16} class="text-primary-400 mt-1 flex-shrink-0" />
              <span>123 Business Ave, Tech City, TC 12345</span>
            </div>
          </div>

          <!-- Social Links -->
          <div class="flex space-x-4">
            {#each socialLinks as social}
              <a 
                href={social.href}
                class="w-10 h-10 bg-gray-800 rounded-lg flex items-center justify-center hover:bg-primary-600 transform hover:scale-110 transition-all duration-300 group"
                aria-label={social.label}
              >
                <svelte:component this={social.icon} size={18} class="text-gray-300 group-hover:text-white" />
              </a>
            {/each}
          </div>
        </div>

        <!-- Quick Links -->
        <div class="space-y-6">
          <h3 class="text-lg font-bold">Quick Links</h3>
          <ul class="space-y-3">
            {#each quickLinks as link}
              <li>
                <button
                  on:click={() => scrollToSection(link.href.slice(1))}
                  class="text-gray-300 hover:text-white hover:translate-x-2 transform transition-all duration-300 block"
                >
                  {link.label}
                </button>
              </li>
            {/each}
          </ul>
        </div>

        <!-- Services -->
        <div class="space-y-6">
          <h3 class="text-lg font-bold">Our Services</h3>
          <ul class="space-y-3">
            {#each services as service}
              <li>
                <button
                  on:click={() => scrollToSection(service.href.slice(1))}
                  class="text-gray-300 hover:text-white hover:translate-x-2 transform transition-all duration-300 block"
                >
                  {service.label}
                </button>
              </li>
            {/each}
          </ul>
        </div>

        <!-- Newsletter -->
        <div class="space-y-6">
          <h3 class="text-lg font-bold">Stay Updated</h3>
          <p class="text-gray-300 text-sm">
            Subscribe to our newsletter for the latest tech insights and company updates.
          </p>
          
          <form class="space-y-3">
            <input
              type="email"
              placeholder="Enter your email"
              class="w-full px-4 py-3 bg-gray-800 border border-gray-700 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300 text-white placeholder-gray-400"
            />
            <button
              type="submit"
              class="w-full bg-gradient-to-r from-primary-600 to-primary-700 hover:from-primary-700 hover:to-primary-800 text-white font-semibold py-3 px-4 rounded-lg shadow-lg hover:shadow-xl transform hover:-translate-y-1 transition-all duration-300 ease-out"
            >
              Subscribe
            </button>
          </form>

          <!-- Trust Badges -->
          <div class="space-y-3">
            <p class="text-gray-400 text-xs">Trusted & Certified</p>
            <div class="flex space-x-3">
              <div class="w-16 h-10 bg-gray-800 rounded flex items-center justify-center">
                <span class="text-xs font-semibold text-gray-300">ISO</span>
              </div>
              <div class="w-16 h-10 bg-gray-800 rounded flex items-center justify-center">
                <span class="text-xs font-semibold text-gray-300">AWS</span>
              </div>
              <div class="w-16 h-10 bg-gray-800 rounded flex items-center justify-center">
                <span class="text-xs font-semibold text-gray-300">SOC2</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Bottom Bar -->
    <div class="border-t border-gray-800">
      <div class="container-custom py-6">
        <div class="flex flex-col md:flex-row justify-between items-center space-y-4 md:space-y-0">
          <div class="text-gray-400 text-sm">
            © {currentYear} Brand IT Solution. All rights reserved.
          </div>
          
          <div class="flex items-center space-x-6 text-sm">
            <a href="/privacy" class="text-gray-400 hover:text-white transition-colors duration-300">
              Privacy Policy
            </a>
            <a href="/terms" class="text-gray-400 hover:text-white transition-colors duration-300">
              Terms of Service
            </a>
            <a href="/cookies" class="text-gray-400 hover:text-white transition-colors duration-300">
              Cookie Policy
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- Scroll to Top Button -->
  <button
    on:click={scrollToTop}
    class="fixed bottom-8 right-8 w-12 h-12 bg-primary-600 hover:bg-primary-700 text-white rounded-full shadow-lg hover:shadow-xl transform hover:scale-110 transition-all duration-300 z-40 flex items-center justify-center group"
    aria-label="Scroll to top"
  >
    <ArrowUp size={20} class="group-hover:-translate-y-1 transition-transform duration-300" />
  </button>
</footer>
