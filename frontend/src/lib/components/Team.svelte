<script lang="ts">
  import { onMount } from 'svelte';
  import { Linkedin, Twitter, Github, Mail } from 'lucide-svelte';

  let teamRef: HTMLElement;
  let isVisible = false;

  // Mouse tracking for 3D flip cards
  const handleMouseMove = (e: MouseEvent, element: HTMLElement) => {
    const rect = element.getBoundingClientRect();
    const centerX = rect.left + rect.width / 2;
    const centerY = rect.top + rect.height / 2;
    
    const mouseX = e.clientX - centerX;
    const mouseY = e.clientY - centerY;
    
    const rotateX = (mouseY / rect.height) * -8;
    const rotateY = (mouseX / rect.width) * 8;
    
    // Apply mouse tracking to the entire card element
    element.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg)`;
  };

  const handleMouseLeave = (element: HTMLElement) => {
    element.style.transform = 'perspective(1000px) rotateX(0deg) rotateY(0deg)';
  };

  const teamMembers = [
    {
      name: 'Sarah Johnson',
      role: 'CEO & Founder',
      bio: 'Visionary leader with 15+ years in technology and business strategy.',
      image: '/api/placeholder/300/400',
      skills: ['Leadership', 'Strategy', 'Innovation'],
      social: {
        linkedin: '#',
        twitter: '#',
        email: 'sarah@brandit.com'
      }
    },
    {
      name: 'Michael Chen',
      role: 'CTO',
      bio: 'Full-stack architect passionate about scalable solutions and emerging technologies.',
      image: '/api/placeholder/300/400',
      skills: ['Architecture', 'Cloud', 'AI/ML'],
      social: {
        linkedin: '#',
        github: '#',
        email: 'michael@brandit.com'
      }
    },
    {
      name: 'Emma Rodriguez',
      role: 'Lead Designer',
      bio: 'Creative designer focused on user experience and beautiful, functional interfaces.',
      image: '/api/placeholder/300/400',
      skills: ['UI/UX', 'Branding', 'Prototyping'],
      social: {
        linkedin: '#',
        twitter: '#',
        email: 'emma@brandit.com'
      }
    },
    {
      name: 'David Kim',
      role: 'Senior Developer',
      bio: 'Backend specialist with expertise in scalable systems and database optimization.',
      image: '/api/placeholder/300/400',
      skills: ['Backend', 'DevOps', 'Security'],
      social: {
        linkedin: '#',
        github: '#',
        email: 'david@brandit.com'
      }
    },
    {
      name: 'Lisa Thompson',
      role: 'Project Manager',
      bio: 'Agile expert ensuring projects are delivered on time and exceed expectations.',
      image: '/api/placeholder/300/400',
      skills: ['Agile', 'Communication', 'Analytics'],
      social: {
        linkedin: '#',
        twitter: '#',
        email: 'lisa@brandit.com'
      }
    },
    {
      name: 'Alex Morgan',
      role: 'DevOps Engineer',
      bio: 'Infrastructure specialist focused on automation, monitoring, and cloud solutions.',
      image: '/api/placeholder/300/400',
      skills: ['AWS', 'Docker', 'Monitoring'],
      social: {
        linkedin: '#',
        github: '#',
        email: 'alex@brandit.com'
      }
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

    if (teamRef) {
      observer.observe(teamRef);
    }

    return () => observer.disconnect();
  });
</script>

<section bind:this={teamRef} class="section-padding bg-gray-50">
  <div class="container-custom">
    <!-- Section Header -->
    <div class="text-center mb-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}">
      <div class="inline-flex items-center space-x-2 bg-primary-100 text-primary-700 px-4 py-2 rounded-full text-sm font-semibold mb-4">
        <span class="w-2 h-2 bg-primary-500 rounded-full"></span>
        <span>Our Team</span>
      </div>
      
      <h2 class="text-responsive-lg font-bold text-gray-900 mb-6">
        Meet the Experts Behind
        <span class="text-gradient">Your Success</span>
      </h2>
      
      <p class="text-xl text-gray-600 max-w-3xl mx-auto">
        Our diverse team of passionate professionals brings together decades of experience 
        in technology, design, and business strategy.
      </p>
    </div>

    <!-- Team Grid -->
    <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
      {#each teamMembers as member, index}
                <!-- Professional 3D Flip Card Container -->
        <div
          role="article"
          class="group perspective-1000 h-96 cursor-pointer {isVisible ? 'animate-fade-in-up' : 'opacity-0'}"
          style="animation-delay: {index * 0.1}s;"
          on:mousemove={(e) => handleMouseMove(e, e.currentTarget)}
          on:mouseleave={(e) => handleMouseLeave(e.currentTarget)}
        >
          <div class="flip-container relative w-full h-full transition-transform duration-700 transform-style-preserve-3d group-hover:rotate-y-180">
            
            <!-- Front of Card -->
            <div class="absolute inset-0 w-full h-full backface-hidden bg-white dark:bg-gray-800 rounded-2xl shadow-lg border border-gray-100 dark:border-gray-700 overflow-hidden">
              <!-- Profile Image -->
              <div class="relative overflow-hidden h-2/3">
                <div class="h-full bg-gradient-to-br from-primary-100 to-secondary-100 flex items-center justify-center relative">
                  <!-- Professional Avatar -->
                  <div class="w-32 h-32 bg-gradient-to-br from-primary-400 to-secondary-400 rounded-full flex items-center justify-center shadow-lg">
                    <span class="text-white text-3xl font-bold">
                      {member.name.split(' ').map(n => n[0]).join('')}
                    </span>
                  </div>
                  
                  <!-- Subtle Background Effects -->
                  <div class="absolute inset-0 opacity-20 pointer-events-none">
                    <div class="absolute top-4 right-4 w-8 h-8 bg-gradient-to-r from-primary-400 to-secondary-400 rounded-full animate-pulse"></div>
                    <div class="absolute bottom-4 left-4 w-6 h-6 bg-gradient-to-r from-secondary-400 to-primary-400 rounded-full animate-pulse" style="animation-delay: 1s;"></div>
                  </div>
                </div>
              </div>

              <!-- Front Content -->
              <div class="p-6 space-y-3 h-1/3 flex flex-col justify-center">
                <div class="text-center">
                  <h3 class="text-lg font-bold text-gray-900 dark:text-white mb-1">{member.name}</h3>
                  <p class="text-primary-600 dark:text-primary-400 font-semibold text-sm">{member.role}</p>
                </div>
                
                <!-- Hover Indicator -->
                <div class="text-center text-primary-500 dark:text-primary-400 text-xs opacity-60">
                  Hover to see more details
                </div>
              </div>
            </div>
            
            <!-- Back of Card -->
            <div class="absolute inset-0 w-full h-full backface-hidden rotate-y-180 bg-gradient-to-br from-primary-400 to-secondary-400 rounded-2xl shadow-lg text-white overflow-hidden">
              <!-- Back Content -->
              <div class="p-6 space-y-4 h-full flex flex-col justify-center">
                <!-- Bio -->
                <div class="text-center">
                  <h3 class="text-lg font-bold mb-2">{member.name}</h3>
                  <p class="text-sm leading-relaxed bg-white/10 backdrop-blur-sm p-3 rounded-lg border border-white/20">
                    {member.bio}
                  </p>
                </div>
                
                <!-- Skills -->
                <div>
                  <h4 class="text-sm font-bold mb-3 text-center">Skills & Expertise</h4>
                  <div class="flex flex-wrap gap-2 justify-center">
                    {#each member.skills as skill}
                      <span class="px-2 py-1 bg-white/20 backdrop-blur-sm text-white text-xs rounded-full font-medium border border-white/30">
                        {skill}
                      </span>
                    {/each}
                  </div>
                </div>
                
                <!-- Social Links -->
                <div class="text-center">
                  <h4 class="text-sm font-bold mb-3">Connect</h4>
                  <div class="flex justify-center space-x-3">
                    {#if member.social.linkedin}
                      <a href={member.social.linkedin} class="w-10 h-10 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center hover:scale-110 transition-transform duration-300 border border-white/30">
                        <Linkedin size={16} />
                      </a>
                    {/if}
                    {#if member.social.twitter}
                      <a href={member.social.twitter} class="w-10 h-10 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center hover:scale-110 transition-transform duration-300 border border-white/30">
                        <Twitter size={16} />
                      </a>
                    {/if}
                    {#if member.social.github}
                      <a href={member.social.github} class="w-10 h-10 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center hover:scale-110 transition-transform duration-300 border border-white/30">
                        <Github size={16} />
                      </a>
                    {/if}
                    {#if member.social.email}
                      <a href="mailto:{member.social.email}" class="w-10 h-10 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center hover:scale-110 transition-transform duration-300 border border-white/30">
                        <Mail size={16} />
                      </a>
                    {/if}
                  </div>
                </div>
              </div>
              
              <!-- Subtle Back Effects -->
              <div class="absolute inset-0 opacity-10 pointer-events-none">
                <div class="absolute top-2 left-2 w-4 h-4 bg-white rounded-full animate-pulse"></div>
                <div class="absolute bottom-2 right-2 w-3 h-3 bg-white rounded-full animate-pulse" style="animation-delay: 1s;"></div>
              </div>
            </div>
          </div>
        </div>
      {/each}
    </div>

    <!-- Join Team CTA -->
    <div class="text-center mt-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.8s;">
      <div class="bg-white rounded-2xl p-8 shadow-lg border border-gray-100 max-w-2xl mx-auto">
        <h3 class="text-2xl font-bold text-gray-900 mb-4">
          Join Our Amazing Team
        </h3>
        <p class="text-gray-600 mb-6">
          We're always looking for talented individuals who share our passion for innovation and excellence.
        </p>
        <div class="flex flex-col sm:flex-row gap-4 justify-center">
          <button class="btn-primary">
            View Open Positions
          </button>
          <button class="btn-secondary">
            Send Your Resume
          </button>
        </div>
      </div>
    </div>

    <!-- Team Stats -->
    <div class="mt-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 1s;">
      <div class="grid grid-cols-2 lg:grid-cols-4 gap-8">
        <div class="text-center">
          <div class="text-3xl font-bold text-primary-600 mb-2">25+</div>
          <div class="text-gray-600">Team Members</div>
        </div>
        <div class="text-center">
          <div class="text-3xl font-bold text-primary-600 mb-2">50+</div>
          <div class="text-gray-600">Certifications</div>
        </div>
        <div class="text-center">
          <div class="text-3xl font-bold text-primary-600 mb-2">15+</div>
          <div class="text-gray-600">Technologies</div>
        </div>
        <div class="text-center">
          <div class="text-3xl font-bold text-primary-600 mb-2">100%</div>
          <div class="text-gray-600">Dedication</div>
        </div>
      </div>
    </div>
  </div>
</section>
