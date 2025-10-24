import Footer from "@/components/Footer";
import Hero from "@/components/Hero";
import Navbar from "@/components/Navbar";

function LandingPage() {
  return (
    <div className="min-h-screen flex flex-col justify-between">
      <Navbar />
      <Hero />
      <Footer />
    </div>
  );
}

export default LandingPage;
