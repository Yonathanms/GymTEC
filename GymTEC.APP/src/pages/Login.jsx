import React, { useState } from 'react';
import api from '../api/axiosConfig';
import { useNavigate } from 'react-router-dom';

function Login() {
  const [correo, setCorreo] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      
      const response = await api.post('/Autentificacion/Login', { correo, password });
      alert(`Bienvenido/a: ${response.data.nombre}`);
      // Aquí podrías redirigir según el tipo de usuario
    } catch (error) {
      alert('Credenciales incorrectas');
    }
  };

  return (
    <div className="container mt-5 col-md-4">
      <h2>Iniciar sesión</h2>
      <input
        type="email"
        className="form-control my-2"
        placeholder="Correo"
        value={correo}
        onChange={(e) => setCorreo(e.target.value)}
      />
      <input
        type="password"
        className="form-control my-2"
        placeholder="Contraseña"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button className="btn btn-primary w-100" onClick={handleLogin}>
        Iniciar sesión
      </button>
      <button className="btn btn-link mt-3" onClick={() => navigate('/registro')}>
        ¿No tienes cuenta? Regístrate
      </button>
    </div>
  );
}

export default Login;
