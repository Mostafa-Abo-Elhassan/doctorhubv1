

import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.jsx';
import './index.css'; 
import AOS from 'aos';
import 'aos/dist/aos.css'; 
import { BrowserRouter } from 'react-router-dom';


AOS.init({
  duration: 1000, 
  once: true,
  easing: 'ease-in-out',
});

// تعيين الاتجاه الافتراضي RTL على وسم HTML
document.documentElement.setAttribute('dir', 'rtl');
document.documentElement.setAttribute('lang', 'ar');

ReactDOM.createRoot(document.getElementById('root')).render(

  <React.StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </React.StrictMode>,
);