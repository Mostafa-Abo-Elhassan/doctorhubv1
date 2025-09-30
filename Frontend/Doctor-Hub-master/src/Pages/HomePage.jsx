import IntroSection from '../components/sections/IntroSection'
import SolutionsSection from '../components/sections/SolutionsSection'
import StoriesSection from '../components/sections/StoriesSection'
import AboutSection from '../components/sections/AboutSection'
import ServicesSection from '../components/sections/ServicesSection'
import FAQVideoSection from '../components/sections/FAQVideoSection'
import ServicesDetailSection from '../components/sections/ServicesDetailSection'
import SupportCenterSection from './SupportCenterSection'

export default function HomePage() {
  return (
      <>
        <IntroSection />
        <SolutionsSection />
        <StoriesSection />
        <AboutSection />
        <ServicesSection />
        <FAQVideoSection />
        <ServicesDetailSection />
        <SupportCenterSection />
      </>
    
  );
}