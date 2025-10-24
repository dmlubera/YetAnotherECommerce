import SignUpForm from "./SignUpForm";

export default function Hero() {
  return (
    <div className="flex-1 flex gap-4">
      <div className="flex-1 flex flex-col justify-center p-8 text-gray-900">
        <h1 className="text-4xl sm:text-5xl font-extrabold leading-tight mb-4">
          Shop the best - delivered fast
        </h1>
        <p className="text-lg text-gray-700 mb-8 max-w-md">
          <strong>yaecommerce</strong> brings top-quality products, exclusive
          offers, and seamless checkout - a modern shopping experience tailored
          to you.
        </p>
      </div>

      <div className="flex-1 flex items-center justify-center p-8 text-gray-900">
        <div className="bg-secondary rounded-md shadow-lg p-8 w-full max-w-md">
          <SignUpForm />
        </div>
      </div>
    </div>
  );
}
