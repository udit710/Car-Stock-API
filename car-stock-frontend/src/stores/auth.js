import { writable } from 'svelte/store';

export const authToken = writable(localStorage.getItem('authToken') || '');
export const username = writable(localStorage.getItem('username') || '');

authToken.subscribe((value) => {
    if (value) {
        localStorage.setItem('authToken', value);
    } else {
        localStorage.removeItem('authToken');
    }
});
