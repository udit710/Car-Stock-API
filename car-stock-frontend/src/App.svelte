<script>
    // import Navbar from './routes/Navbar.svelte';
    import { authToken } from './stores/auth';
    import { currentRoute } from './stores.js';
    import { onMount } from 'svelte';
    
    import Login from './routes/Login.svelte';
    import AddCar from './routes/AddCar.svelte';
    import SearchCar from './routes/SearchCar.svelte';
    import Register from './routes/Register.svelte';

		let route;
        $: $currentRoute = currentRoute;
	
        onMount(() => {
            const handleHashChange = () => currentRoute.set(window.location.hash || '/');
            window.addEventListener('hashchange', handleHashChange);
            handleHashChange();
            return () => window.removeEventListener('hashchange', handleHashChange);
        });
	</script>

<!-- <Navbar /> -->
<body>
<h1>Car Stock API</h1>
<nav>
{#if $authToken}
    <button>
        <a href="#/list-cars">List Cars</a>
    </button>
    <button>
        <a href="#/add-car">Add Car</a>
    </button>
    <button on:click={() => authToken.set('')}>
        <a href="/">Logout</a>
    </button>
{:else}
    <button>
        <a href="/">Login</a>
    </button>
    <button>
        <a href="#/register">Register</a>
    </button>
{/if}
</nav>

<br>
{#if $currentRoute === '/' && !$authToken}
    <Login />
{:else if $currentRoute === '/' && $authToken}
    <Login />
{:else if $currentRoute === '#/register'}
    <Register />
{:else if $currentRoute === '#/add-car'}
    <AddCar />
{:else if $currentRoute === '#/list-cars' && $authToken}
    <SearchCar />
{:else}
    <h1>404 Not Found</h1>
{/if}

</body>

<style>
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        padding: 0;
    }

    h1 {
        text-align: center;
    }

    nav {
        display: flex;
        justify-content: center;
        margin-bottom: 20px;
    }

    button {
        margin: 0 10px;
    }

    a {
        text-decoration: none;
        color: inherit;
    }
</style>