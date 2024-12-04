<script>
    import { authToken } from '../stores/auth';
    import axios from 'axios';
    import { onMount } from 'svelte';

    let make = '';
    let model = '';
    let stockCount = 0;
    let cars = [];
    let showConfirmDelete = false;
    let carToDelete = null;
    let stockCountMap = {};

    onMount(getAllCars);

    async function getAllCars() {
        try {
            const token = $authToken;
            const response = await axios.get('http://localhost:5000/cars', {
                headers: { Authorization: `Bearer ${token}` }
            });
            cars = response.data;
        } catch (error) {
            alert('Failed to fetch cars.');
        }
    }

    async function searchCar() {
        try {
            const token = $authToken;
            const response = await axios.get('http://localhost:5000/cars/search', {
                params: { make, model },
                headers: { Authorization: `Bearer ${token}` }
            });

            cars = response.data;
        } catch (error) {
            alert('Failed to search for car.');
        }
    }

    async function updateStockCount(car) {
        try {
            const id = parseInt(car.carId);
            stockCount = stockCountMap[id];
            console.log(id);
            const token = $authToken;
            await axios.put(`http://localhost:5000/cars/${id}/stock`, { stockCount }, {
                headers: { Authorization: `Bearer ${token}` }
            });
            alert('Car stock updated!');
            searchCar();
            stockCountMap[id] = '';
        } catch (error) {
            alert('Failed to update car stock.');
        }
    }

    function confirmDelete(car) {
        carToDelete = car;
        showConfirmDelete = true;
    }

    async function deleteCar() {
        if (carToDelete) {
            try {
                const id = parseInt(carToDelete.carId);
                const token = $authToken;
                await axios.delete(`http://localhost:5000/cars/${id}`, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                alert('Car deleted!');
                searchCar();
            } catch (error) {
                alert('Failed to delete car.');
            } finally {
                showConfirmDelete = false;
                carToDelete = null;
            }
        }
    }

    function cancelDelete() {
        showConfirmDelete = false;
        carToDelete = null;
    }
</script>

<form on:submit|preventDefault={searchCar}>
    <h1>My Cars</h1>
    <button on:click={() => getAllCars()} >Show all Cars </button>
    <h2>Search Car</h2>
    <input type="text" placeholder="Make" bind:value={make} />
    <input type="text" placeholder="Model" bind:value={model} />
    <button type="submit">Search Car</button>

    {#if cars.length === 0}
        <p>No cars found.</p>
    {/if}
    <ul>
        {#each cars as car}
            <li>
                Make: {car.make} 
                <br>
                Model: {car.model} 
                <br>
                Year: {car.year} 
                <br>
                Current stock: {car.stockCount} 
                <br>
                <span style="color: blue;">Enter new stock count to update:</span>
                <input type="number" placeholder="New Stock Count" bind:value={stockCountMap[car.carId]} /> 
                <button on:click={() => updateStockCount(car)}>Update Stock</button>
                <br>
                <button on:click={() => confirmDelete(car)}>Delete Car</button>
            </li>
        {/each}
    </ul>
</form>

{#if showConfirmDelete}
    <div class="confirm-popup">
        <h3>Confirm Delete</h3>
        <p>Are you sure you want to delete the car with the following details?</p>
        <p><strong>Make:</strong> {carToDelete.make}</p>
        <p><strong>Model:</strong> {carToDelete.model}</p>
        <p><strong>Year:</strong> {carToDelete.year}</p>
        <button on:click={deleteCar}>Yes, Delete</button>
        <button on:click={cancelDelete}>Cancel</button>
    </div>
{/if}

<style>
    form {
        margin: 30px 120px;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 8px;
        box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
    }
    ul {
        list-style-type: none;
        padding: 0;
        margin: 20px 0;
    }
    li {
        margin-bottom: 1rem;
    }
    .confirm-popup {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: white;
        border: 1px solid #ccc;
        padding: 20px;
        border-radius: 8px;
        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);
    }
    .confirm-popup button {
        margin-right: 10px;
    }
</style>
