import { useState } from 'react';
import './App.css';
//importacion de bootsrap
import 'bootstrap/dist/css/bootstrap.min.css';

const initialPersona = {
    NumCedula: '',
    Nombre: '',
    Apellido1: '',
    Apellido2: '',
    FechaNacimiento: '',
    Provincia: '',
    Canton: '',
    Distrito: '',
    CorreoElectronico: '',
    Password: ''
};

function App() {
    const [view, setView] = useState('home');
    const [persona, setPersona] = useState(initialPersona);
    const [message, setMessage] = useState('');

    const apiUrl = import.meta.env.VITE_API_URL;

    const handleInputChange = e => {
        const { name, value } = e.target;
        setPersona(prev => ({ ...prev, [name]: value }));
    };

    const handleRegister = async e => {
        e.preventDefault();
        try {
            const response = await fetch(`${apiUrl}/api/Personas`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(persona)
            });
            if (response.ok) {
                setMessage('¡Persona registrada exitosamente!');
                setPersona(initialPersona);
            } else {
                const err = await response.text();
                setMessage('Error al registrar: ' + err);
            }
        } catch (error) {
            setMessage('Error de red: ' + error.message);
        }
    };

    return (
        <div className="container mt-5">
            <h1>Bienvenido a GymTEC</h1>
            {view === 'home' && (
                <div>
                    <button className="btn btn-primary m-2" onClick={() => setView('login')}>Login</button>
                    <button className="btn btn-success m-2" onClick={() => setView('register')}>Register</button>
                </div>
            )}

            {view === 'register' && (
                <form onSubmit={handleRegister} className="mt-4">
                    <h2>Registro de Persona</h2>
                    <input className="form-control mb-2" type="text" name="NumCedula" placeholder="Cédula" value={persona.NumCedula} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="text" name="Nombre" placeholder="Nombre" value={persona.Nombre} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="text" name="Apellido1" placeholder="Primer Apellido" value={persona.Apellido1} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="text" name="Apellido2" placeholder="Segundo Apellido" value={persona.Apellido2} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="date" name="FechaNacimiento" placeholder="Fecha de Nacimiento" value={persona.FechaNacimiento} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="text" name="Provincia" placeholder="Provincia" value={persona.Provincia} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="text" name="Canton" placeholder="Cantón" value={persona.Canton} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="text" name="Distrito" placeholder="Distrito" value={persona.Distrito} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="email" name="CorreoElectronico" placeholder="Correo Electrónico" value={persona.CorreoElectronico} onChange={handleInputChange} required />
                    <input className="form-control mb-2" type="password" name="Password" placeholder="Contraseña" value={persona.Password} onChange={handleInputChange} required />
                    <button className="btn btn-success mt-2" type="submit">Registrar</button>
                    <button className="btn btn-secondary mt-2 ms-2" type="button" onClick={() => { setView('home'); setPersona(initialPersona); }}>Volver</button>
                    {message && <div className="alert alert-info mt-3">{message}</div>}
                </form>
            )}

            {view === 'login' && (
                <div className="mt-4">
                    <h2>Login (pendiente de implementación)</h2>
                    <button className="btn btn-secondary mt-2" onClick={() => setView('home')}>Volver</button>
                </div>
            )}
        </div>
    );
}

export default App;