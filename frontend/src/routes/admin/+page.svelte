<script lang="ts">
  import { onMount } from 'svelte';
  import { 
    Users, 
    Mail, 
    Settings, 
    FileText, 
    TrendingUp, 
    Clock,
    MessageSquare,
    CheckCircle
  } from 'lucide-svelte';
  import { api } from '$lib/services/api';

  let stats = {
    totalServices: 0,
    totalTeamMembers: 0,
    totalBlogPosts: 0,
    totalMessages: 0,
    newMessages: 0,
    respondedMessages: 0
  };

  let recentMessages: any[] = [];
  let isLoading = true;

  onMount(async () => {
    try {
      // Load dashboard stats
      const [contactStats, messages] = await Promise.all([
        api.getContactStats(),
        api.getContactMessages({ page: 1, pageSize: 5 })
      ]);

      stats = {
        totalServices: 6, // Static for demo
        totalTeamMembers: 6, // Static for demo
        totalBlogPosts: 6, // Static for demo
        totalMessages: contactStats.Total,
        newMessages: contactStats.New,
        respondedMessages: contactStats.Responded
      };

      recentMessages = messages;
    } catch (error) {
      console.error('Error loading dashboard data:', error);
    } finally {
      isLoading = false;
    }
  });

  const statCards = [
    {
      title: 'Total Services',
      value: stats.totalServices,
      icon: Settings,
      color: 'bg-blue-500',
      change: '+12%',
      changeType: 'positive'
    },
    {
      title: 'Team Members',
      value: stats.totalTeamMembers,
      icon: Users,
      color: 'bg-green-500',
      change: '+3%',
      changeType: 'positive'
    },
    {
      title: 'Blog Posts',
      value: stats.totalBlogPosts,
      icon: FileText,
      color: 'bg-purple-500',
      change: '+8%',
      changeType: 'positive'
    },
    {
      title: 'Total Messages',
      value: stats.totalMessages,
      icon: Mail,
      color: 'bg-orange-500',
      change: '+24%',
      changeType: 'positive'
    }
  ];

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      month: 'short',
      day: 'numeric',
      year: 'numeric'
    });
  };

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'New': return 'bg-blue-100 text-blue-800';
      case 'InProgress': return 'bg-yellow-100 text-yellow-800';
      case 'Responded': return 'bg-green-100 text-green-800';
      case 'Closed': return 'bg-gray-100 text-gray-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  };
</script>

<svelte:head>
  <title>Dashboard - Admin Panel</title>
</svelte:head>

<div class="space-y-6">
  <!-- Page Header -->
  <div class="flex items-center justify-between">
    <div>
      <h1 class="text-2xl font-bold text-gray-900">Dashboard</h1>
      <p class="text-gray-600">Welcome back! Here's what's happening with your business.</p>
    </div>
    <div class="text-sm text-gray-500">
      Last updated: {new Date().toLocaleString()}
    </div>
  </div>

  {#if isLoading}
    <div class="flex items-center justify-center py-12">
      <div class="spinner"></div>
    </div>
  {:else}
    <!-- Stats Grid -->
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
      {#each statCards as card}
        <div class="bg-white rounded-xl shadow-lg p-6 border border-gray-100 hover:shadow-xl transition-shadow duration-300">
          <div class="flex items-center justify-between">
            <div>
              <p class="text-sm font-medium text-gray-600">{card.title}</p>
              <p class="text-3xl font-bold text-gray-900 mt-2">{card.value}</p>
              <div class="flex items-center mt-2">
                <TrendingUp size={16} class="text-green-500 mr-1" />
                <span class="text-sm text-green-600 font-medium">{card.change}</span>
                <span class="text-sm text-gray-500 ml-1">vs last month</span>
              </div>
            </div>
            <div class="w-12 h-12 {card.color} rounded-lg flex items-center justify-center">
              <svelte:component this={card.icon} size={24} class="text-white" />
            </div>
          </div>
        </div>
      {/each}
    </div>

    <!-- Content Grid -->
    <div class="grid lg:grid-cols-3 gap-6">
      <!-- Recent Messages -->
      <div class="lg:col-span-2 bg-white rounded-xl shadow-lg border border-gray-100">
        <div class="p-6 border-b border-gray-100">
          <div class="flex items-center justify-between">
            <h2 class="text-xl font-bold text-gray-900">Recent Messages</h2>
            <a href="/admin/messages" class="text-primary-600 hover:text-primary-700 text-sm font-medium">
              View all
            </a>
          </div>
        </div>
        <div class="p-6">
          {#if recentMessages.length === 0}
            <div class="text-center py-8">
              <MessageSquare size={48} class="text-gray-300 mx-auto mb-4" />
              <p class="text-gray-500">No messages yet</p>
            </div>
          {:else}
            <div class="space-y-4">
              {#each recentMessages as message}
                <div class="flex items-start space-x-4 p-4 bg-gray-50 rounded-lg hover:bg-gray-100 transition-colors duration-200">
                  <div class="w-10 h-10 bg-primary-100 rounded-full flex items-center justify-center flex-shrink-0">
                    <span class="text-primary-600 font-semibold text-sm">
                      {message.Name.charAt(0).toUpperCase()}
                    </span>
                  </div>
                  <div class="flex-1 min-w-0">
                    <div class="flex items-center justify-between">
                      <h4 class="text-sm font-semibold text-gray-900 truncate">
                        {message.Name}
                      </h4>
                      <span class="px-2 py-1 text-xs font-medium rounded-full {getStatusColor(message.Status)}">
                        {message.Status}
                      </span>
                    </div>
                    <p class="text-sm text-gray-600 truncate mt-1">{message.Subject}</p>
                    <div class="flex items-center mt-2 text-xs text-gray-500">
                      <Clock size={12} class="mr-1" />
                      {formatDate(message.CreatedAt)}
                    </div>
                  </div>
                </div>
              {/each}
            </div>
          {/if}
        </div>
      </div>

      <!-- Quick Actions & Stats -->
      <div class="space-y-6">
        <!-- Message Stats -->
        <div class="bg-white rounded-xl shadow-lg border border-gray-100 p-6">
          <h3 class="text-lg font-bold text-gray-900 mb-4">Message Overview</h3>
          <div class="space-y-4">
            <div class="flex items-center justify-between">
              <div class="flex items-center space-x-2">
                <div class="w-3 h-3 bg-blue-500 rounded-full"></div>
                <span class="text-sm text-gray-600">New Messages</span>
              </div>
              <span class="text-sm font-semibold text-gray-900">{stats.newMessages}</span>
            </div>
            <div class="flex items-center justify-between">
              <div class="flex items-center space-x-2">
                <div class="w-3 h-3 bg-green-500 rounded-full"></div>
                <span class="text-sm text-gray-600">Responded</span>
              </div>
              <span class="text-sm font-semibold text-gray-900">{stats.respondedMessages}</span>
            </div>
            <div class="flex items-center justify-between">
              <div class="flex items-center space-x-2">
                <div class="w-3 h-3 bg-gray-300 rounded-full"></div>
                <span class="text-sm text-gray-600">Total</span>
              </div>
              <span class="text-sm font-semibold text-gray-900">{stats.totalMessages}</span>
            </div>
          </div>
        </div>

        <!-- Quick Actions -->
        <div class="bg-white rounded-xl shadow-lg border border-gray-100 p-6">
          <h3 class="text-lg font-bold text-gray-900 mb-4">Quick Actions</h3>
          <div class="space-y-3">
            <a 
              href="/admin/services/new" 
              class="block w-full bg-primary-600 hover:bg-primary-700 text-white text-center py-2 px-4 rounded-lg transition-colors duration-200"
            >
              Add New Service
            </a>
            <a 
              href="/admin/team/new" 
              class="block w-full bg-green-600 hover:bg-green-700 text-white text-center py-2 px-4 rounded-lg transition-colors duration-200"
            >
              Add Team Member
            </a>
            <a 
              href="/admin/blog/new" 
              class="block w-full bg-purple-600 hover:bg-purple-700 text-white text-center py-2 px-4 rounded-lg transition-colors duration-200"
            >
              Create Blog Post
            </a>
          </div>
        </div>

        <!-- System Status -->
        <div class="bg-white rounded-xl shadow-lg border border-gray-100 p-6">
          <h3 class="text-lg font-bold text-gray-900 mb-4">System Status</h3>
          <div class="space-y-3">
            <div class="flex items-center justify-between">
              <span class="text-sm text-gray-600">API Status</span>
              <div class="flex items-center space-x-2">
                <CheckCircle size={16} class="text-green-500" />
                <span class="text-sm text-green-600">Online</span>
              </div>
            </div>
            <div class="flex items-center justify-between">
              <span class="text-sm text-gray-600">Database</span>
              <div class="flex items-center space-x-2">
                <CheckCircle size={16} class="text-green-500" />
                <span class="text-sm text-green-600">Connected</span>
              </div>
            </div>
            <div class="flex items-center justify-between">
              <span class="text-sm text-gray-600">Email Service</span>
              <div class="flex items-center space-x-2">
                <CheckCircle size={16} class="text-green-500" />
                <span class="text-sm text-green-600">Active</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  {/if}
</div>
