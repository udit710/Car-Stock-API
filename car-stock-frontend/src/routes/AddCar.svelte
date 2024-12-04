<script>
    import { authToken } from '../stores/auth';
    import axios from 'axios';

    let make = '';
    let model = '';
    let year = '';
    let stockCount = 0;

    async function addCar() {
        try {
            const token = $authToken;
            await axios.post('http://localhost:5000/cars', { make, model, year, stockCount }, {
                headers: { Authorization: `Bearer ${token}` }
            });
            alert('Car added successfully!');
        } catch (error) {
            alert('Failed to add car.');
        }
    }
</script>

<form on:submit|preventDefault={addCar}>
    <h1>Add Car</h1>
    <br>
    Make:
    <input type="text" placeholder="Make" bind:value={make} />
    <br>
    Model:
    <input type="text" placeholder="Model" bind:value={model} />
    <br>
    Year:
    <input type="number" placeholder="Year" bind:value={year} />
    <br>
    Stock Count:
    <input type="number" placeholder="Stock Count" bind:value={stockCount} />
    <br>
    <button type="submit">Add Car</button>
</form>

<style>
    form {
        max-width: 1000px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 8px;
        box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
    }
</style>