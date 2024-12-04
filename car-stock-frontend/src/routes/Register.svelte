<script>
    import axios from 'axios';
    let username = '';
    let password = '';
    let errorMessage = '';
    let successMessage = '';

    async function registerUser() {
        // Clear previous messages
        errorMessage = '';
        successMessage = '';

        // Client-side validation
        if (password.length < 6) {
            errorMessage = 'Password must be at least 6 characters long.';
            return;
        }

        try {
            // Send POST request to register endpoint
            const response = await axios.post('http://localhost:5000/auth/register', {
                username,
                password
            });

            // Success message
            successMessage = 'Registration successful! You can now log in.';
            username = '';
            password = '';
        } catch (error) {
            // Handle API errors
            if (error.response && error.response.data.message === 'Username already exists') {
                errorMessage = 'This username is already taken. Please choose another.';
            } else {
                errorMessage = 'An error occurred during registration. Please try again.';
            }
        }
    }
</script>

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
        /* color: white; */
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }
    button:disabled {
        cursor: not-allowed;
    }
    .message {
        margin-top: 10px;
        font-size: 14px;
    }
    .error {
        color: red;
    }
    .success {
        color: green;
    }
</style>

<form on:submit|preventDefault={registerUser}>
    <h1>Register</h1>

    <label for="username">Username:</label>
    <input
        id="username"
        type="text"
        bind:value={username}
        placeholder="Enter your username"
        required />

    <label for="password">Password:</label>
    <input
        id="password"
        type="password"
        bind:value={password}
        placeholder="Enter a password (min 6 chars)"
        required />

    <button type="submit" disabled={!username || !password}>Register</button>

    {#if errorMessage}
        <div class="message error">{errorMessage}</div>
    {/if}

    {#if successMessage}
        <div class="message success">{successMessage}</div>
    {/if}
</form>
