import { Button } from "./ui/button";
import logo from "./../../public/vite.svg";

const Navbar = () => {
  return (
    <nav className="px-10 py-4 border border-border">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <img src={logo} />
          <span className="font-heading text-xl font-bold">yaecommerce</span>
        </div>
        <Button variant="outline">Sign In</Button>
      </div>
    </nav>
  );
};

export default Navbar;
