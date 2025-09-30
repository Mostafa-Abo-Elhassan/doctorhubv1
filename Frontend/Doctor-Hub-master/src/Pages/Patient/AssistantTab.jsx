import React, { useState } from 'react';
import { FaPaperPlane, FaRobot } from 'react-icons/fa';

const AssistantTab = () => {
  const [messages, setMessages] = useState([
    { id: 1, text: 'أهلاً بك في المساعد الذكي! كيف يمكنني مساعدتك اليوم؟ يمكنك أن تسألني عن مواعيدك، روشتاتك، أو نتائج تحاليلك.', sender: 'bot' }
  ]);
  const [inputValue, setInputValue] = useState('');

  const handleSendMessage = () => {
    if (inputValue.trim() === '') return;

    // إضافة رسالة المستخدم
    const userMessage = { id: Date.now(), text: inputValue, sender: 'user' };
    setMessages(prev => [...prev, userMessage]);
    setInputValue('');

    // محاكاة رد البوت (يمكنك لاحقاً ربطها بـ API حقيقي)
    setTimeout(() => {
      const botResponse = { id: Date.now() + 1, text: '   سوف يتم التواصل معك قريباً ...', sender: 'bot' };
      setMessages(prev => [...prev, botResponse]);
    }, 1000);
  };

  return (
    <div className="p-4 sm:p-6 h-full flex flex-col" style={{maxHeight: '85vh'}}>
      <div className="flex items-center mb-6">
        <FaRobot className="w-8 h-8 text-teal-500" />
        <h2 className="text-2xl font-bold text-gray-800 mr-3">🤖 المساعد الذكي</h2>
      </div>

      {/* منطقة عرض الرسائل */}
      <div className="flex-grow bg-white rounded-xl shadow-inner p-4 overflow-y-auto space-y-4">
        {messages.map(message => (
          <div key={message.id} className={`flex items-end gap-2 ${message.sender === 'user' ? 'justify-end' : 'justify-start'}`}>
            {message.sender === 'bot' && <div className="w-8 h-8 bg-gray-200 rounded-full flex items-center justify-center flex-shrink-0"><FaRobot className="w-5 h-5 text-gray-500" /></div>}
            <div className={`max-w-xs lg:max-w-md px-4 py-2 rounded-2xl ${
              message.sender === 'user' 
                ? 'bg-blue-500 text-white rounded-br-none' 
                : 'bg-gray-100 text-gray-800 rounded-bl-none'
            }`}>
              <p>{message.text}</p>
            </div>
          </div>
        ))}
      </div>

      {/* منطقة إدخال النص */}
      <div className="mt-4 flex items-center gap-2">
        <input
          type="text"
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          onKeyPress={(e) => e.key === 'Enter' && handleSendMessage()}
          placeholder="اسألني عن روشتتك أو نتائج التحاليل..."
          className="flex-grow p-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-teal-500"
        />
        <button 
          onClick={handleSendMessage}
          className="bg-teal-500 text-white p-3 rounded-xl hover:bg-teal-600 transition-colors disabled:bg-gray-300"
          disabled={!inputValue.trim()}
        >
          <FaPaperPlane className="w-5 h-5" />
        </button>
      </div>
    </div>
  );
};

export default AssistantTab;
