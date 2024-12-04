<script>
    import { authToken } from '../stores/auth';
    import axios from 'axios';

    // username to be passed to localstorage
    let username = '';
    let password = '';

    async function login() {
        try {
            const response = await axios.post('http://localhost:5000/auth/login', { username, password });
            authToken.set(response.data.token);
            alert('Login successful!');

            // set username to localstorage
            localStorage.setItem('username', username);
        } catch (error) {
            alert('Login failed. Please try again.');
            console.error(error);
        }
    }
</script>

<form on:submit|preventDefault={login}>
    <h1>Login</h1>

    {#if !$authToken}
    <input type="text" placeholder="Username" bind:value={username} />
    <input type="password" placeholder="Password" bind:value={password} />
    <button type="submit">Login</button>

    <p>Don't have an account? <a href="#/register">Register</a></p>

    {:else if $authToken}
        <p>You are logged in. <button on:click={() => authToken.set('')}>Logout</button></p>
    {/if}
</form>

<style>
    form {
        max-width: 400px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 8px;
        box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
    }
    input {
        display: block;
        width: 100%;
        margin-bottom: 10px;
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 4px;
    }
    button {
        padding: 10px 15px;
        background-color: #007bff;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }
    button:disabled {
        background-color: #ccc;
        cursor: not-allowed;
    }
</style>