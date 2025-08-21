<script lang="ts">
  import { onMount } from 'svelte';
  import { Calendar, User, ArrowRight, Tag } from 'lucide-svelte';

  let blogRef: HTMLElement;
  let isVisible = false;

  const blogPosts = [
    {
      id: 1,
      title: 'The Future of Cloud Computing: Trends to Watch in 2024',
      excerpt: 'Explore the latest cloud computing trends that are shaping the future of business technology and how they can benefit your organization.',
      author: 'Michael Chen',
      date: '2024-01-15',
      category: 'Cloud Computing',
      readTime: '5 min read',
      image: '/api/placeholder/400/250',
      tags: ['Cloud', 'Technology', 'Trends']
    },
    {
      id: 2,
      title: 'Cybersecurity Best Practices for Small Businesses',
      excerpt: 'Learn essential cybersecurity measures that every small business should implement to protect their digital assets and customer data.',
      author: 'Sarah Johnson',
      date: '2024-01-12',
      category: 'Cybersecurity',
      readTime: '7 min read',
      image: '/api/placeholder/400/250',
      tags: ['Security', 'Business', 'Best Practices']
    },
    {
      id: 3,
      title: 'Building Scalable Web Applications with Modern Frameworks',
      excerpt: 'Discover the best practices for creating web applications that can grow with your business using cutting-edge development frameworks.',
      author: 'David Kim',
      date: '2024-01-10',
      category: 'Web Development',
      readTime: '6 min read',
      image: '/api/placeholder/400/250',
      tags: ['Web Development', 'Scalability', 'Frameworks']
    },
    {
      id: 4,
      title: 'Data Analytics: Turning Information into Business Intelligence',
      excerpt: 'Learn how to leverage data analytics to make informed business decisions and drive growth through actionable insights.',
      author: 'Emma Rodriguez',
      date: '2024-01-08',
      category: 'Data Analytics',
      readTime: '8 min read',
      image: '/api/placeholder/400/250',
      tags: ['Data', 'Analytics', 'Business Intelligence']
    },
    {
      id: 5,
      title: 'Mobile App Development: Native vs Cross-Platform',
      excerpt: 'Compare the advantages and disadvantages of native and cross-platform mobile app development to make the right choice for your project.',
      author: 'Alex Morgan',
      date: '2024-01-05',
      category: 'Mobile Development',
      readTime: '6 min read',
      image: '/api/placeholder/400/250',
      tags: ['Mobile', 'Development', 'Apps']
    },
    {
      id: 6,
      title: 'Digital Transformation: A Step-by-Step Guide',
      excerpt: 'Navigate your digital transformation journey with our comprehensive guide covering strategy, implementation, and best practices.',
      author: 'Lisa Thompson',
      date: '2024-01-03',
      category: 'Digital Transformation',
      readTime: '10 min read',
      image: '/api/placeholder/400/250',
      tags: ['Digital Transformation', 'Strategy', 'Business']
    }
  ];

  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { 
      year: 'numeric', 
      month: 'long', 
      day: 'numeric' 
    });
  };

  const getCategoryColor = (category: string) => {
    const colors: { [key: string]: string } = {
      'Cloud Computing': 'bg-blue-100 text-blue-800',
      'Cybersecurity': 'bg-red-100 text-red-800',
      'Web Development': 'bg-green-100 text-green-800',
      'Data Analytics': 'bg-purple-100 text-purple-800',
      'Mobile Development': 'bg-orange-100 text-orange-800',
      'Digital Transformation': 'bg-pink-100 text-pink-800'
    };
    return colors[category] || 'bg-gray-100 text-gray-800';
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

    if (blogRef) {
      observer.observe(blogRef);
    }

    return () => observer.disconnect();
  });
</script>

<section bind:this={blogRef} class="section-padding bg-white">
  <div class="container-custom">
    <!-- Section Header -->
    <div class="text-center mb-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}">
      <div class="inline-flex items-center space-x-2 bg-primary-100 text-primary-700 px-4 py-2 rounded-full text-sm font-semibold mb-4">
        <span class="w-2 h-2 bg-primary-500 rounded-full"></span>
        <span>Latest Insights</span>
      </div>
      
      <h2 class="text-responsive-lg font-bold text-gray-900 mb-6">
        Technology Insights &
        <span class="text-gradient">Industry News</span>
      </h2>
      
      <p class="text-xl text-gray-600 max-w-3xl mx-auto">
        Stay informed with the latest trends, best practices, and insights from our team of 
        technology experts and industry thought leaders.
      </p>
    </div>

    <!-- Featured Post -->
    <div class="mb-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 0.2s;">
      <div class="bg-gradient-to-r from-primary-600 to-secondary-600 rounded-2xl p-8 md:p-12 text-white relative overflow-hidden">
        <!-- Background Pattern -->
        <div class="absolute inset-0 opacity-10">
          <div class="absolute inset-0 bg-[url('data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNjAiIGhlaWdodD0iNjAiIHZpZXdCb3g9IjAgMCA2MCA2MCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48ZyBmaWxsPSJub25lIiBmaWxsLXJ1bGU9ImV2ZW5vZGQiPjxnIGZpbGw9IiNmZmZmZmYiIGZpbGwtb3BhY2l0eT0iMC4yIj48Y2lyY2xlIGN4PSIzMCIgY3k9IjMwIiByPSI0Ii8+PC9nPjwvZz48L3N2Zz4=')]"></div>
        </div>

        <div class="relative z-10 grid lg:grid-cols-2 gap-8 items-center">
          <div class="space-y-6">
            <div class="flex items-center space-x-4">
              <span class="px-3 py-1 bg-white/20 rounded-full text-sm font-semibold">
                Featured
              </span>
              <span class="px-3 py-1 bg-white/20 rounded-full text-sm">
                {blogPosts[0].category}
              </span>
            </div>
            
            <h3 class="text-2xl lg:text-3xl font-bold leading-tight">
              {blogPosts[0].title}
            </h3>
            
            <p class="text-blue-100 text-lg leading-relaxed">
              {blogPosts[0].excerpt}
            </p>

            <div class="flex items-center space-x-6 text-blue-200">
              <div class="flex items-center space-x-2">
                <User size={16} />
                <span class="text-sm">{blogPosts[0].author}</span>
              </div>
              <div class="flex items-center space-x-2">
                <Calendar size={16} />
                <span class="text-sm">{formatDate(blogPosts[0].date)}</span>
              </div>
              <span class="text-sm">{blogPosts[0].readTime}</span>
            </div>

            <button class="group bg-white text-primary-600 hover:bg-blue-50 font-semibold py-3 px-6 rounded-lg transition-all duration-300 flex items-center space-x-2">
              <span>Read Full Article</span>
              <ArrowRight size={16} class="group-hover:translate-x-1 transition-transform duration-300" />
            </button>
          </div>

          <div class="relative">
            <div class="aspect-video bg-white/10 rounded-xl backdrop-blur-sm border border-white/20 flex items-center justify-center">
              <div class="text-center space-y-2">
                <div class="w-16 h-16 bg-white/20 rounded-full flex items-center justify-center mx-auto">
                  <Tag size={24} />
                </div>
                <p class="text-sm opacity-80">Featured Article</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Blog Grid -->
    <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
      {#each blogPosts.slice(1) as post, index}
        <article 
          class="group bg-white rounded-2xl shadow-lg hover:shadow-2xl transform hover:-translate-y-2 transition-all duration-500 ease-out overflow-hidden border border-gray-100 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}"
          style="animation-delay: {0.4 + index * 0.1}s;"
        >
          <!-- Image -->
          <div class="aspect-video bg-gradient-to-br from-gray-100 to-gray-200 relative overflow-hidden">
            <div class="absolute inset-0 bg-gradient-to-br from-primary-500/20 to-secondary-500/20 flex items-center justify-center">
              <div class="text-center space-y-2">
                <div class="w-12 h-12 bg-white/20 rounded-full flex items-center justify-center mx-auto">
                  <Tag size={20} class="text-primary-600" />
                </div>
                <p class="text-xs text-gray-600">Article Image</p>
              </div>
            </div>
            
            <!-- Category Badge -->
            <div class="absolute top-4 left-4">
              <span class="px-3 py-1 {getCategoryColor(post.category)} rounded-full text-xs font-semibold">
                {post.category}
              </span>
            </div>
          </div>

          <!-- Content -->
          <div class="p-6 space-y-4">
            <h3 class="text-xl font-bold text-gray-900 group-hover:text-primary-600 transition-colors duration-300 line-clamp-2">
              {post.title}
            </h3>
            
            <p class="text-gray-600 text-sm leading-relaxed line-clamp-3">
              {post.excerpt}
            </p>

            <!-- Tags -->
            <div class="flex flex-wrap gap-2">
              {#each post.tags.slice(0, 2) as tag}
                <span class="px-2 py-1 bg-gray-100 text-gray-600 text-xs rounded-md">
                  {tag}
                </span>
              {/each}
            </div>

            <!-- Meta -->
            <div class="flex items-center justify-between pt-4 border-t border-gray-100">
              <div class="flex items-center space-x-3 text-gray-500 text-xs">
                <div class="flex items-center space-x-1">
                  <User size={12} />
                  <span>{post.author}</span>
                </div>
                <div class="flex items-center space-x-1">
                  <Calendar size={12} />
                  <span>{formatDate(post.date)}</span>
                </div>
              </div>
              
              <span class="text-xs text-gray-500">{post.readTime}</span>
            </div>

            <!-- Read More -->
            <button class="group w-full flex items-center justify-center space-x-2 text-primary-600 hover:text-primary-700 font-semibold py-2 transition-colors duration-300">
              <span>Read More</span>
              <ArrowRight size={14} class="group-hover:translate-x-1 transition-transform duration-300" />
            </button>
          </div>
        </article>
      {/each}
    </div>

    <!-- View All Button -->
    <div class="text-center mt-12 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 1s;">
      <button class="btn-primary">
        View All Articles
      </button>
    </div>

    <!-- Newsletter Signup -->
    <div class="mt-16 {isVisible ? 'animate-fade-in-up' : 'opacity-0'}" style="animation-delay: 1.2s;">
      <div class="bg-gray-50 rounded-2xl p-8 text-center max-w-2xl mx-auto">
        <h3 class="text-2xl font-bold text-gray-900 mb-4">
          Stay Updated with Our Latest Insights
        </h3>
        <p class="text-gray-600 mb-6">
          Subscribe to our newsletter and never miss the latest technology trends and business insights.
        </p>
        
        <form class="flex flex-col sm:flex-row gap-4 max-w-md mx-auto">
          <input
            type="email"
            placeholder="Enter your email address"
            class="flex-1 px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-300"
          />
          <button
            type="submit"
            class="bg-primary-600 hover:bg-primary-700 text-white font-semibold py-3 px-6 rounded-lg transition-colors duration-300 whitespace-nowrap"
          >
            Subscribe
          </button>
        </form>
      </div>
    </div>
  </div>
</section>
