import React from 'react';
import { Routes, Route } from 'react-router-dom'
import './App.css';
import LoginPage from './components/auth/login';
import SignupPage from './components/auth/signup';
import MyAccountPage from './components/auth/account';
import CategoryCreatePage from './components/create';
import Layout from './components/layout';
import HomePage from './components/layout/home';
import UserPage from './components/users/list';



function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<HomePage/>} />
          <Route path="features" element={<h1>Features Page</h1>} />
          <Route path="pricing" element={<h1>Pricing Page</h1>} />
          <Route path="faqs" element={<h1>FAQs Page</h1>} />
          <Route path="about" element={<h1>About Page</h1>} />
          <Route path="login" element={<LoginPage/>} />
          <Route path="signup" element={<SignupPage/>} />
          <Route path="profile" element={<MyAccountPage/>} />
          <Route path="category/create" element={<CategoryCreatePage />} />
          <Route path="users" element={<UserPage/>} />
        </Route>
      </Routes>
    </>
  );
}

export default App;
